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

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Insights.OutputClasses;
using Microsoft.Azure.Management.Monitor;
using Microsoft.Azure.Management.Monitor.Models;
using Microsoft.WindowsAzure.Commands.Common.CustomAttributes;

namespace Microsoft.Azure.Commands.Insights.Diagnostics
{
    [CmdletDeprecation(ReplacementCmdletName = "Get-AzEventCategory")]
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SubscriptionDiagnosticSettingCategory"), OutputType(typeof(PSSubscriptionDiagnosticSettingCategory))]
    public class GetAzureRmSubscriptionDiagnosticSettingCategoryCommand : ManagementCmdletBase
    {
        protected override void ProcessRecordInternal()
        {
            IEnumerable<LocalizableString> rawData = this.MonitorManagementClient.EventCategories.List();
            WriteObject(rawData.Select(value => new PSSubscriptionDiagnosticSettingCategory(value.Value, PSDiagnosticSettingCategoryType.Logs)).ToList(), true); ;
        }
    }
}
