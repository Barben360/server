﻿using System;
using System.Collections.Generic;
using Bit.Core.Models.Data;
using Bit.Core.Utilities;
using Newtonsoft.Json;

namespace Bit.Core.Models.Table
{
    public class Cipher : ITableObject<Guid>, ICloneable
    {
        private Dictionary<string, CipherAttachment.MetaData> _attachmentData;

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrganizationId { get; set; }
        public Enums.CipherType Type { get; set; }
        public string Data { get; set; }
        public string Favorites { get; set; }
        public string Folders { get; set; }
        public string Attachments { get; set; }
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;
        public DateTime RevisionDate { get; internal set; } = DateTime.UtcNow;
        public DateTime? DeletedDate { get; internal set; }
        public Enums.CipherRepromptType? Reprompt { get; set; }

        public void SetNewId()
        {
            Id = CoreHelpers.GenerateComb();
        }

        public Dictionary<string, CipherAttachment.MetaData> GetAttachments()
        {
            if (string.IsNullOrWhiteSpace(Attachments))
            {
                return null;
            }

            if (_attachmentData != null)
            {
                return _attachmentData;
            }

            try
            {
                _attachmentData = JsonConvert.DeserializeObject<Dictionary<string, CipherAttachment.MetaData>>(Attachments);
                foreach (var kvp in _attachmentData)
                {
                    kvp.Value.AttachmentId = kvp.Key;
                }
                return _attachmentData;
            }
            catch
            {
                return null;
            }
        }

        public void SetAttachments(Dictionary<string, CipherAttachment.MetaData> data)
        {
            if (data == null || data.Count == 0)
            {
                _attachmentData = null;
                Attachments = null;
                return;
            }

            _attachmentData = data;
            Attachments = JsonConvert.SerializeObject(_attachmentData);
        }

        public void AddAttachment(string id, CipherAttachment.MetaData data)
        {
            var attachments = GetAttachments();
            if (attachments == null)
            {
                attachments = new Dictionary<string, CipherAttachment.MetaData>();
            }

            attachments.Add(id, data);
            SetAttachments(attachments);
        }

        public void DeleteAttachment(string id)
        {
            var attachments = GetAttachments();
            if (!attachments?.ContainsKey(id) ?? true)
            {
                return;
            }

            attachments.Remove(id);
            SetAttachments(attachments);
        }

        public bool ContainsAttachment(string id)
        {
            var attachments = GetAttachments();
            return attachments?.ContainsKey(id) ?? false;
        }

        object ICloneable.Clone() => Clone();
        public Cipher Clone()
        {
            var clone = CoreHelpers.CloneObject(this);
            clone.CreationDate = CreationDate;
            clone.RevisionDate = RevisionDate;

            return clone;
        }
    }
}
