﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using System.Reflection;
using Module = Autofac.Module;
using DataAccess.Context;
using Core.DataAccess.MongoOptions;
using Microsoft.Extensions.Options;
using Core.DataAccess;
using DataAccess.Repository;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDB;
using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching;
using Microsoft.Extensions.DependencyInjection;
using Business.ThirdPartyServices.PaymentServices;
using Business.ThirdPartyServices.PaymentServices.IyziPay;
using Business.ThirdPartyServices.MessageBrokerServices;
using Business.ThirdPartyServices.MessageBrokerServices.RabbitMQ;
using Entities.Dtos;
using Business.ThirdPartyServices.StorageServices;
using Autofac.Core;
using Business.ThirdPartyServices.StorageServices.Azure;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
       
        protected override void Load(ContainerBuilder builder)
        {
            //Mongo

            builder.RegisterType<MSurveyDal>().As<ISurveyDal>().SingleInstance();
            builder.RegisterType<SurveyManager>().As<ISurveyService>().SingleInstance();

            builder.RegisterType<MSolvedSurveyDal>().As<ISolvedSurveyDal>().SingleInstance();

            builder.RegisterType<MSurveyFilterDal>().As<ISurveyFilterDal>().SingleInstance();
            builder.RegisterType<SurveyFilterManager>().As<ISurveyFilterService>().SingleInstance();

            builder.RegisterType<MWatchedAdDal>().As<IWatchedAdDal>().SingleInstance();

            builder.RegisterType<MAdDal>().As<IAdDal>().SingleInstance();
            builder.RegisterType<AdManager>().As<IAdService>().SingleInstance();

            builder.RegisterType<MAdFilterDal>().As<IAdFilterDal>().SingleInstance();
            builder.RegisterType<AdFilterManager>().As<IAdFilterService>().SingleInstance();

            //MSql

            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();

            builder.RegisterType<CardManager>().As<ICardService>().SingleInstance();
            builder.RegisterType<EfCardDal>().As<ICardDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //services

            builder.RegisterType<PaymentManager>().As<IPaymentService>().SingleInstance();

            builder.RegisterType<ContactManager>().As<IContactService>().SingleInstance();


            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

            //Third Party Services

            builder.RegisterGeneric(typeof(RabbitMQAdapter<>)).As(typeof(IMessageBrokerService<>)).InstancePerDependency();

            builder.RegisterType<AzureStorageAdapter>().As<IStorageService>().SingleInstance();

            builder.RegisterType<IyzipayAdapter>().As<IThirdPartyPaymentService>().SingleInstance();
        }

    }
}