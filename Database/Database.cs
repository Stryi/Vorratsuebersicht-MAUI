using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorratsUebersicht
{
    public static class Database
    {

        internal static string[] GetCategoriesInUse()
        {
            /*
            SQLite.SQLiteConnection databaseConnection = Android_Database.Instance.GetConnection();

            // Artikel suchen, die schon abgelaufen sind.
            string cmd = string.Empty;
            cmd += "SELECT DISTINCT Category AS Value";
            cmd += " FROM Article";
            cmd += " WHERE Category IS NOT NULL";
            cmd += " AND ArticleId IN (SELECT ArticleId FROM StorageItem)";
            cmd += " ORDER BY Category COLLATE NOCASE";

            var command = databaseConnection.CreateCommand(cmd);
            IList<StringResult> result = command.ExecuteQuery<StringResult>();

            string[] stringList = new string[result.Count];
            for(int i = 0; i < result.Count; i++)
            {
                stringList[i] = result[i].Value;
            }
            */

            string[] stringList = new string[3];
            stringList[0] = "Trinken";
            stringList[1] = "Essen";
            stringList[2] = "Gesundheit";
            return stringList;
        }

    }
}
