using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Ninject;
using RecipeSite.Domain.Abstract;
using RecipeSite.Domain.Concrete;
using RecipeSite.Domain.Entities;

namespace RecipeSite.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ICustomerRepository>().To<EFCustomerRepository>();
            kernel.Bind<IIngredientCategoryRepository>().To<EFIngredientCategoryRepository>();
            kernel.Bind<IRecipeIngredientListRepository>().To<EFRecipeIngredientListRepository>();
            kernel.Bind<IRecipeIngredientRepository>().To<EFRecipeIngredientRepository>();
            kernel.Bind<IRecipeMeasurementRepository>().To<EFRecipeMeasurementRepository>();
            kernel.Bind<IRecipeQuantityRepository>().To<EFRecipeQuantityRepository>();
            kernel.Bind<IRecipeRepository>().To<EFRecipeRepository>();
        }
    }
}