﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ServiceBus.Models;
using System.Collections;
using System.Management.Automation;
using System.Collections.Generic;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;

namespace Microsoft.Azure.Commands.ServiceBus.Commands.Queue
{
    /// <summary>
    /// 'Get-AzServiceBusQueue' Cmdlet gives the details of a / List of ServiceBus Queue(s)
    /// <para> If Queue name provided, a single Queue detials will be returned</para>
    /// <para> If Queue name not provided, list of Queue will be returned</para>
    /// </summary>
    [GenericBreakingChange(message: BreakingChangeNotification + "\n- Output type of the cmdlet would change to 'Microsoft.Azure.PowerShell.Cmdlets.ServiceBus.Models.Api202201Preview.ISbQueue'", deprecateByVersion: DeprecateByVersion, changeInEfectByDate: ChangeInEffectByDate)]
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "ServiceBusQueue"), OutputType(typeof(PSQueueAttributes))]
    public class GetAzureRmServiceBusQueue : AzureServiceBusCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 0, HelpMessage = "The name of the resource group")]
        [ResourceGroupCompleter]
        [Alias("ResourceGroup")]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 1, HelpMessage = "Namespace Name")]
        [Alias(AliasNamespaceName)]
        [ValidateNotNullOrEmpty]
        public string Namespace { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, Position = 1, HelpMessage = "Queue Name")]
        [Alias(AliasQueueName)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [CmdletParameterBreakingChange("MaxCount", ChangeDescription = "'-MaxCount' is being removed. '-Skip' and '-Top' would be added to support pagination.")]
        [Parameter(Mandatory = false, HelpMessage = "Determine the maximum number of Queues to return.")]
        [ValidateRange(1, 10000)]
        public int? MaxCount { get; set; }

        public override void ExecuteCmdlet()
        {
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    var queueAttributes = Client.GetQueue(ResourceGroupName, Namespace, Name);
                    WriteObject(queueAttributes);
                }
                else
                {
                    if (MaxCount.HasValue)
                    {
                        IEnumerable<PSQueueAttributes> queueAttributes = Client.ListQueues(ResourceGroupName, Namespace, MaxCount);
                        WriteObject(queueAttributes, true);
                    }
                    else
                    {
                        IEnumerable<PSQueueAttributes> queueAttributes = Client.ListQueues(ResourceGroupName, Namespace);
                        WriteObject(queueAttributes, true);
                    }

                }
            }
            catch (Management.ServiceBus.Models.ErrorResponseException ex)
            {
                WriteError(ServiceBusClient.WriteErrorforBadrequest(ex));
            }
        }
    }
}
