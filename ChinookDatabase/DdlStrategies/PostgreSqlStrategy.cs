﻿using System.Data;
using System.Text;

namespace ChinookDatabase.DdlStrategies
{
    public class PostgreSqlStrategy : AbstractDdlStrategy
    {
        public PostgreSqlStrategy()
        {
            var builder = new StringBuilder();

            builder.AppendLine("SET \"PGOPTIONS=-c client_min_messages=WARNING\"")
				.AppendLine("dropdb --if-exists -U postgres Chinook")
				.AppendLine("createdb -U postgres Chinook")
				.AppendLine("psql -f {0} -q Chinook postgres");

            Name = "PostgreSql";
            IsReCreateDatabaseEnabled = true;
            CommandLineFormat = builder.ToString();
        }

        public override string FormatName(string name) => $"{name}";

        public override string GetStoreType(DataColumn column) => column.DataType.ToString() switch
        {
            "System.String" => $"VARCHAR({column.MaxLength})",
            "System.Int32" => "INT",
            "System.Decimal" => "NUMERIC(10,2)",
            "System.DateTime" => "TIMESTAMP",
            _ => "error_" + column.DataType
        };
    }
}
