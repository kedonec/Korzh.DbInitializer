﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

/// <summary>
/// Contains all classes which implement exporting operations.
/// </summary>
namespace Korzh.DbUtils.Export
{
    public interface IDatasetExporter
    {
        void Export(IDataReader reader, Stream outStream, string datasetName = null);

        string FormatExtension { get; }
    }
}