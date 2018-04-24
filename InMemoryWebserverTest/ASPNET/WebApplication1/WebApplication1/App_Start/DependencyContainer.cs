using Unity;
using WebApplication1.Business;

namespace WebApplication1
{
   public static class DependencyContainer
   {
      public static IUnityContainer GetContainer()
      {
         var unityContainer = new UnityContainer();

         unityContainer.RegisterType<IUserManager, UserManager>();

         return unityContainer;
      }
   }
}