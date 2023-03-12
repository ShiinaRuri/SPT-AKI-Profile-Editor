﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SPT_AKI_Profile_Editor.Core.HelperClasses
{
    public class TemplateEntity
    {
        public TemplateEntity(string id,
                              Dictionary<string, IComparable> values,
                              List<TemplateEntity> templateEntities)
        {
            Id = id;
            Values = values;
            TemplateEntities = templateEntities;
        }

        public string Id { get; }
        public Dictionary<string, IComparable> Values { get; }
        public List<TemplateEntity> TemplateEntities { get; }

        [JsonIgnore]
        public bool NotEmpty => Values?.Count > 0 || TemplateEntities?.Count > 0;

        private static JsonSerializerSettings SerializerSettings => new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static TemplateEntity Load(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<TemplateEntity>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {

                Logger.Log($"Template load error: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public void Save(string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, SerializerSettings);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {

                Logger.Log($"Template save error: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}