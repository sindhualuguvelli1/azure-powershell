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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.AdvancedThreatProtection.Model;
using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.AdvancedThreatProtection.Cmdlet
{
    /// <summary>
    /// Disables the Advanced Data Security of a specific server.
    /// </summary>
    [GenericBreakingChange("Disable-AzSqlServerAdvancedThreatProtection alias will be removed in an upcoming breaking change release", "9.0.0")]
    [Cmdlet("Disable", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlServerAdvancedDataSecurity", SupportsShouldProcess = true), OutputType(typeof(ServerAdvancedDataSecurityPolicyModel))]
    [Alias("Disable-AzSqlServerAdvancedThreatProtection")]
    public class DisableAzureSqlServerAdvancedDataSecurity : SqlServerAdvancedDataSecurityCmdletBase
    {
        /// <summary>
        /// This method is responsible to call the right API in the communication layer that will eventually send the information in the 
        /// object to the REST endpoint
        /// </summary>
        /// <param name="model">The model object with the data to be sent to the REST endpoints</param>
        protected override ServerAdvancedDataSecurityPolicyModel PersistChanges(ServerAdvancedDataSecurityPolicyModel model)
        {
            model.IsEnabled = false;
            ModelAdapter.SetServerAdvancedDataSecurity(model, DefaultContext.Environment.GetEndpoint(AzureEnvironment.Endpoint.StorageEndpointSuffix));
            return model;
        }
    }
}
