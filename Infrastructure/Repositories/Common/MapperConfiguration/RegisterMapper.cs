﻿#nullable enable
using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Infrastructure.Repositories.Common.MapperConfiguration
{
    public class RegisterMapper:Profile
    {
        public RegisterMapper()
        {
            this.ApplyMappingProfiles(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingProfiles(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICreateMapper<>)))
                .ToList();

            foreach (var type in types)
            {
                var model = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Map") //get the map method directly by the class
                                 ?? type.GetInterface("ICreateMapper`1").GetMethod("Map"); //if null get the interface implementation

                if (model != null)
                    methodInfo?.Invoke(model, new object?[] {this});
            }
        }
    }
}
