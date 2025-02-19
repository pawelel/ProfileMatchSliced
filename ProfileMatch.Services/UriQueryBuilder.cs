﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileMatch.Services
{
    public class UriQueryBuilder
    {
        private readonly StringBuilder _builder = new();

        public bool AppendParameter(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
                return false;

            bool isFirstParameter = !_builder.ToString().Contains('?');
            string escapedValue = Uri.EscapeDataString(value);

            _builder.Append(isFirstParameter ? '?' : '&').Append(name).Append('=').Append(escapedValue);
            return true;
        }

        public void AppendParameters(IDictionary<string, string> parameters)
        {
            foreach ((string name, string value) in parameters)
                AppendParameter(name, value);
        }

        public override string ToString()
            => _builder.ToString();
    }
}