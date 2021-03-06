// <copyright file="PreOperationnew_weekly_report_detailDelete.cs" company="">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author></author>
// <date>2020-01-08 오후 5:39:52</date>
// <summary>Implements the PreOperationnew_weekly_report_detailDelete Plugin.</summary>
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
// </auto-generated>

using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace CellCrmVSSolution1.CellCRMPlugin
{

    /// <summary>
    /// PreOperationnew_weekly_report_detailDelete Plugin.
    /// </summary>    
    public class PreOperationnew_weekly_report_detailDelete: PluginBase
    {

        private static readonly object SyncLock = new object();
        /// <summary>
        /// Initializes a new instance of the <see cref="PreOperationnew_weekly_report_detailDelete"/> class.
        /// </summary>
        /// <param name="unsecure">Contains public (unsecured) configuration information.</param>
        /// <param name="secure">Contains non-public (secured) configuration information. 
        /// When using Microsoft Dynamics 365 for Outlook with Offline Access, 
        /// the secure string is not passed to a plug-in that executes while the client is offline.</param>
        public PreOperationnew_weekly_report_detailDelete(string unsecure, string secure)
            : base(typeof(PreOperationnew_weekly_report_detailDelete))
        {
            
           // TODO: Implement your custom configuration handling.
        }


        /// <summary>
        /// Main entry point for he business logic that the plug-in is to execute.
        /// </summary>
        /// <param name="localContext">The <see cref="LocalPluginContext"/> which contains the
        /// <see cref="IPluginExecutionContext"/>,
        /// <see cref="IOrganizationService"/>
        /// and <see cref="ITracingService"/>
        /// </param>
        /// <remarks>
        /// For improved performance, Microsoft Dynamics 365 caches plug-in instances.
        /// The plug-in's Execute method should be written to be stateless as the constructor
        /// is not called for every invocation of the plug-in. Also, multiple system threads
        /// could execute the plug-in at the same time. All per invocation state information
        /// is stored in the context. This means that you should not use global variables in plug-ins.
        /// </remarks>
        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new InvalidPluginExecutionException("localContext");
            }

            try
            {
                IPluginExecutionContext context = localContext.PluginExecutionContext;
                IOrganizationService service = localContext.OrganizationService;


                if (context.IsInTransaction)
                {
                    lock (SyncLock)
                    {

                        if (context.Depth < 2)
                        {
                            if (context.InputParameters["Target"] is Entity)
                            {
                                Entity target = (Entity)context.InputParameters["Target"];


                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw new InvalidPluginExecutionException("delete fail");
            }



        }
    }
}
