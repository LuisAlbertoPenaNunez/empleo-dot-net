using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using EmpleoDotNet.AppServices;
using EmpleoDotNet.Data;
using EmpleoDotNet.Repository;
using EmpleoDotNet.Repository.Contracts;
using Ninject.Modules;
using Ninject.Web.Common; 

namespace EmpleadoDotNet.MobileAPI.App_Start
{
    public class EmpleadoModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<EmpleadoContext>().ToSelf().InRequestScope();
            Kernel.Bind<IJobOpportunityRepository>().To<JobOpportunityRepository>();
            Kernel.Bind<ITagRepository>().To<TagRepository>();
            Kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            Kernel.Bind<IJobOpportunityService>().To<JobOpportunityService>();
            Kernel.Bind<IUserProfileSocialService>().To<UserProfileSocialService>();
            Kernel.Bind<IJobOpportunityLikeService>().To<JobOpportunityLikeService>();
            Kernel.Bind<IJobOpportunityLikeRepository>().To<JobOpportunityLikeRepository>();
        }
    }
}