using System;
using System.Collections.Generic;
using System.Text;

namespace yixiaozi.DataHandler.sqlite
{
    public class SQLiteTable
    {
        public string TableName = "";
        public SQLiteColumnList Columns = new SQLiteColumnList();

        public SQLiteTable()
        { }

        public SQLiteTable(string name)
        {
            TableName = name;
        }
    }
}