﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DocearReminder
{
    public partial class TagListControl : UserControl
    {
        public string mindmapPath = "";
        public string nodeID = "";
        public event EventHandler TagChanged;

        private HashSet<string> _tags;

        public int Count
        {
            get { return _tags.Count; }
        }

        public List<string> Tags
        {
            get
            {
                return _tags.ToList();
            }

            set
            {
                value = value ?? new List<string>();
                Clear();

                value.ForEach(x => _tags.Add(x));
                RebuildTagList();
            }
        }

        private void RebuildTagList()
        {
            txtTag.Text = "";
            foreach (var tag in _tags.OrderBy(x=>x)) {
                AddTagLabel(tag);
            }
        }

        private void AddTag(string tag)
        {
            if(_tags.Add(tag.Trim()))
                AddTagLabel(tag);
            TagChanged(null, null);
        }

        private void AddTagLabel(string tag) {
            var tagLabel = new TagLabelControl(tag);
            tagLabel.Name = GetTagControlName(tag);
            tagLabel.TabStop = false;
            tagPanel.Controls.Add(tagLabel);
            tagLabel.DeleteClicked += TagLabel_DeleteClicked;
            tagLabel.DoubleClicked += TagLabel_DoubleClicked;
        }

        private void RemoveTag(string tag)
        {
            _tags.Remove(tag);
            var tagControl = tagPanel.Controls.Find(GetTagControlName(tag), true)[0];
            tagPanel.Controls.Remove(tagControl);
            TagChanged(null, null);
        }

        private void TagLabel_DeleteClicked(object sender, string tag)
        {
            RemoveTag(tag);
        }

        private void TagLabel_DoubleClicked(object sender, string tag)
        {
            RemoveTag(tag);
            txtTag.Text = tag;
            txtTag.Focus();
            txtTag.SelectionStart = txtTag.TextLength;
        }

        private string GetTagControlName(string tag)
        {
            return "tagLabel_" + tag;
        }

        public void Clear()
        {
            _tags.Clear();
            while(tagPanel.Controls.Count > 1)
                tagPanel.Controls.RemoveAt(1);
        }

        public TagListControl()
        {
            InitializeComponent();
            txtTag.Width= tagPanel.Width - 6;
            _tags = new HashSet<string>();
            Clear();
        }

        private void txtTag_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                var text = txtTag.Text.Trim();

                if (text.StartsWith("del"))
                {
                    try
                    {
                        int i = Convert.ToInt16(text.Replace("del", ""));
                        RemoveTag(_tags.ElementAt(i-1));
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (text == "clear") {
                    Clear();
                    TagChanged(null, null);
                }
                else
                {
                    if (!string.IsNullOrEmpty(text)) AddTag(text);
                }
                txtTag.Text = "";
            }
        }

    }
}
