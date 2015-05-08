using System;
using System.Web.Mvc;
using StructureMap;

namespace ProblemTest
{
    public class StructureMapModelBinderProvider : IModelBinderProvider
    {
        private readonly IContainer _container;

        public StructureMapModelBinderProvider(IContainer container)
        {
            _container = container;
        }

        public IModelBinder GetBinder(Type modelType)
        {
            var mappings = _container.GetInstance<ModelBinderMappingDictionary>();
            if (mappings != null && mappings.ContainsKey(modelType))
                return _container.GetInstance(mappings[modelType]) as IModelBinder;

            return null;
        }
    }
}