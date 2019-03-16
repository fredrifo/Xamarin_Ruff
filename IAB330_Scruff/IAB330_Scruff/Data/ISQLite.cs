using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAB330_Scruff.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
