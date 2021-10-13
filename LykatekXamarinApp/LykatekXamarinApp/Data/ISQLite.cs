using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LykatecMobileApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
