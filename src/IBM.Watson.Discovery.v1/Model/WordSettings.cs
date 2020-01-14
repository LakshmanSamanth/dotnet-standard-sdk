/**
* (C) Copyright IBM Corp. 2018, 2020.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using Newtonsoft.Json;

namespace IBM.Watson.Discovery.v1.Model
{
    /// <summary>
    /// A list of Word conversion settings.
    /// </summary>
    public class WordSettings
    {
        /// <summary>
        /// Object containing heading detection conversion settings for Microsoft Word documents.
        /// </summary>
        [JsonProperty("heading", NullValueHandling = NullValueHandling.Ignore)]
        public WordHeadingDetection Heading { get; set; }
    }

}
