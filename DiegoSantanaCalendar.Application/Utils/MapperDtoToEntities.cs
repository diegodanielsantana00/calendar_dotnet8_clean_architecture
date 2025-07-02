using DiegoSantanaCalendar.Domain.Interfaces.Base; // Assumindo que IBaseRepository está aqui
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.Utils
{
    public class MapperDtoToEntities
    {
        private readonly IServiceProvider _serviceProvider;

        public MapperDtoToEntities(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TDestination> MapToExistingAsync<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);

            foreach (PropertyInfo sourceProp in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                PropertyInfo destinationProp = destinationType.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);

                if (destinationProp != null && destinationProp.CanWrite)
                {
                    object? sourceValue = sourceProp.GetValue(source);

                    if (sourceValue == null && sourceProp.PropertyType.IsGenericType && sourceProp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        continue;
                    }
                    else if (sourceValue == null && sourceProp.PropertyType == typeof(string))
                    {
                        continue;
                    }

                    if (destinationProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destinationProp.SetValue(destination, sourceValue);
                    }
                    else
                    {
                        if (sourceProp.Name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) && sourceProp.PropertyType == typeof(Guid?))
                        {
                            Guid? idValue = (Guid?)sourceValue;

                            if (idValue.HasValue)
                            {
                                string entityName = sourceProp.Name.Replace("Id", string.Empty);
                                Type entityType = destinationProp.PropertyType;
                                Type genericBaseRepositoryType = typeof(IBaseRepository<>).MakeGenericType(entityType);
                                Type? repositoryInterfaceType = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(s => s.GetTypes())
                                    .Where(p => p.IsInterface && p.Name == $"I{entityName}Repository" && genericBaseRepositoryType.IsAssignableFrom(p))
                                    .FirstOrDefault();

                                if (repositoryInterfaceType != null)
                                {
                                    var repository = _serviceProvider.GetService(repositoryInterfaceType);

                                    if (repository != null)
                                    {
                                        MethodInfo? getByIdMethod = repositoryInterfaceType.GetMethod("GetByIdAsync");
                                        if (getByIdMethod != null)
                                        {
                                            var task = (Task?)getByIdMethod.Invoke(repository, new object[] { idValue.Value });
                                            if (task != null)
                                            {
                                                await task;
                                                var resultProperty = task.GetType().GetProperty("Result");
                                                object? relatedEntity = resultProperty?.GetValue(task);

                                                if (relatedEntity != null)
                                                {
                                                    destinationProp.SetValue(destination, relatedEntity);
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Aviso: Entidade relacionada '{entityName}' com ID '{idValue.Value}' não encontrada.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Aviso: Método 'GetByIdAsync' não encontrado na interface '{repositoryInterfaceType.Name}'.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Aviso: Repositório '{repositoryInterfaceType.Name}' não resolvido pelo ServiceProvider.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Aviso: Não foi possível encontrar a interface do repositório para '{entityName}' do tipo '{entityType.Name}'.");
                                }
                            }
                            else if (!idValue.HasValue && destinationProp.PropertyType.IsClass)
                            {
                                destinationProp.SetValue(destination, null);
                            }
                        }
                    }
                }
            }
            return destination;
        }
    }
}