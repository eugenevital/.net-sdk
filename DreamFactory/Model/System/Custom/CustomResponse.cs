﻿namespace DreamFactory.Model.System.Custom
{
    using DreamFactory.Model.System.User;
    using global::System;
    using Newtonsoft.Json;

    /// <summary>
    /// CustomResponse.
    /// </summary>
    public class CustomResponse
    {
        /// <summary>
        /// Id of the custom setting.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// Id of the user linked to this custom setting.
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        /// <summary>
        /// Name of the custom setting.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Value of the custom setting.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Date when the custom setting was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_date")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Date when the custom setting was modified.
        /// </summary>
        [JsonProperty(PropertyName = "last_modified_date")]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// UserId that created the custom setting.
        /// </summary>
        [JsonProperty(PropertyName = "created_by_id")]
        public int? CreatedById { get; set; }

        /// <summary>
        /// UserId that modified the custom setting.
        /// </summary>
        [JsonProperty(PropertyName = "last_modified_by_id")]
        public int? LastModifiedById { get; set; }

        /// <summary>
        /// A single User record that this record potentially belongs to.
        /// </summary>
        [JsonProperty(PropertyName = RelatedResources.Custom.UserCreated)]
        public RelatedUser UserCreated { get; set; }

        /// <summary>
        /// A single User record that this record potentially belongs to.
        /// </summary>
        [JsonProperty(PropertyName = RelatedResources.Custom.UserLastModified)]
        public RelatedUser UserLastModified { get; set; }

        /// <summary>
        /// A single User record that this record potentially belongs to.
        /// </summary>
        [JsonProperty(PropertyName = RelatedResources.Custom.User)]
        public RelatedUser User { get; set; }
    }
}
