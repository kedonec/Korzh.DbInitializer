﻿using System;

using Korzh.DbUtils.Packing;

namespace Korzh.DbUtils.Import
{

    public class DbImporter
    {
        private readonly IDbWriter _dbWriter;
        private readonly IDatasetImporter _datasetImporter;
        private readonly IDataUnpacker _dataUnpacker;

        public DbImporter(IDbWriter dbWriter, IDatasetImporter datasetImporter, IDataUnpacker unpacker)
        {
            _dbWriter = dbWriter;
            _datasetImporter = datasetImporter;
            _dataUnpacker = unpacker;
        }

        public void Import()
        {
            _dataUnpacker.StartUnpacking();
            try {
                while (_dataUnpacker.HasData()) {
                    using (var datasetStream = _dataUnpacker.OpenNextStreamForUnpacking()) {
                        var dataset = _datasetImporter.StartImport(datasetStream);
                        Console.WriteLine($"Reading {dataset.Name}..."); //!!!!!!!!!!!!!!!!!!!!!
                        while (_datasetImporter.HasRecords()) {
                            _dbWriter.WriteRecord(_datasetImporter.NextRecord());
                        }
                        _datasetImporter.FinishImport();
                        Console.WriteLine("");
                    }
                }
            }
            finally {
                _dataUnpacker.FinishUnpacking();
            }
        }
    }

}