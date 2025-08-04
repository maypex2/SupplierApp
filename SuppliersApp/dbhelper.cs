    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    namespace SuppliersApp
    {
        public static class dbhelper
        {
            private static string sharedFolder = @"C:\DBHOST\SharedFolder\";
            private static string dbPath = Path.Combine(sharedFolder, "suppliers.db");
            private static string connectionString = $"Data Source={dbPath};Version=3;Pooling=True;Max Pool Size=100;";


        public static void InitializeDatabase()
        {
            if (!Directory.Exists(sharedFolder))
            {
                try
                {
                    Directory.CreateDirectory(sharedFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating shared folder: {ex.Message}");
                }
            }

        
            string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
            Username TEXT PRIMARY KEY,
            Password TEXT NOT NULL
            )";
            ExecuteNonQuery(createUsersTable);

            // Add default users
            AddUser("DapCWD", "Liza");
            AddUser("dapcwd-melanie", "melanie123");
        }

        public static bool AddUser(string username, string password)
        {
            string sql = "INSERT OR IGNORE INTO Users (Username, Password) VALUES (@user, @pass)";
            int rowsAffected = ExecuteNonQuery(sql,
                new SQLiteParameter("@user", username),
                new SQLiteParameter("@pass", password));
            return rowsAffected > 0;
        }

        public static void ReplaceAllCredentials(string newUsername, string newPassword)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete all existing credentials
                        string deleteSql = "DELETE FROM Users";
                        using (var cmd = new SQLiteCommand(deleteSql, connection, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // Add the new credentials
                        string insertSql = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";
                        using (var cmd = new SQLiteCommand(insertSql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@user", newUsername);
                            cmd.Parameters.AddWithValue("@pass", newPassword);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error updating credentials: {ex.Message}");
                    }
                }
            }
        }


        public static bool ValidateUser(string username, string password)
            {
                string sql = "SELECT COUNT(*) FROM Users WHERE Username = @user AND Password = @pass";
                object result = ExecuteScalar(sql,
                    new SQLiteParameter("@user", username),
                    new SQLiteParameter("@pass", password));
                return result != null && Convert.ToInt32(result) > 0;
            }

            public static int AddCategory(string categoryType)
            {
                string checkSql = "SELECT Id FROM Categories WHERE category_type = @categoryType";
                object existingId = ExecuteScalar(checkSql, new SQLiteParameter("@categoryType", categoryType));

                if (existingId != null) return Convert.ToInt32(existingId);

                string insertSql = "INSERT INTO Categories (category_type) VALUES (@categoryType); SELECT last_insert_rowid();";
                object result = ExecuteScalar(insertSql, new SQLiteParameter("@categoryType", categoryType));

                return result != null ? Convert.ToInt32(result) : 0;
            }



            public static bool AddSupplier(string name, int categoryId, string representative,
                                    string phone, string address, string email, string philgeps)
            {
                string sql = @"INSERT INTO Suppliers 
              (Name, CategoryId, Supplier_Representative, Phone, Address, Email, Philgeps, DateRegistered, DateChanged) 
              VALUES (@name, @categoryId, @representative, @phone, @address, @email, @philgeps, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

                int rowsAffected = ExecuteNonQuery(sql,
                    new SQLiteParameter("@name", name),
                    new SQLiteParameter("@categoryId", categoryId),
                    new SQLiteParameter("@representative", representative ?? (object)DBNull.Value),
                    new SQLiteParameter("@phone", phone ?? (object)DBNull.Value),
                    new SQLiteParameter("@address", address ?? (object)DBNull.Value),
                    new SQLiteParameter("@email", email ?? (object)DBNull.Value),
                    new SQLiteParameter("@philgeps", philgeps ?? (object)DBNull.Value));

                return rowsAffected > 0;
            }

            public static DataTable GetAllCategories()
            {
                string sql = "SELECT Id, category_type FROM Categories ORDER BY category_type";
                return GetDataTable(sql);
            }


            public static string GetCategoryNameById(int id)
            {
                string sql = "SELECT category_type FROM Categories WHERE Id = @id";
                return ExecuteScalar(sql, new SQLiteParameter("@id", id))?.ToString();
            }




            public static DataTable SearchSuppliers1(string cate, string supp)
            {
                string sql = "";
                if (cate == "All Categories" && string.IsNullOrWhiteSpace(supp))
                {
                    sql = @"SELECT s.Id, s.Name, s.CategoryId, 
                  s.Supplier_Representative, s.Phone, s.Address, s.Email, s.Philgeps 
                  FROM Suppliers s ORDER BY s.Id ASC";
                }
                else if (cate == "All Categories" && !string.IsNullOrWhiteSpace(supp))
                {
                    sql = @"SELECT s.* FROM Suppliers s
                   JOIN Categories c ON s.CategoryId = c.Id
                   WHERE s.Name LIKE @searcha
                   ORDER BY s.Name ASC";
                }
                else
                {
                    // Use EXACT MATCH for category
                    sql = @"SELECT s.* FROM Suppliers s
                   JOIN Categories c ON s.CategoryId = c.Id
                   WHERE s.Name LIKE @searcha AND c.category_type = @searchb
                   ORDER BY s.Name ASC";
                }

                return GetDataTable(sql,
                    new SQLiteParameter("@searcha", $"%{supp}%"),
                    // Remove wildcards for exact category match
                    new SQLiteParameter("@searchb", cate));
            }



            public static DataTable GetSuppliersForExactCategory(string category)
                {
                    const string sql = @"SELECT s.Id, s.Name, s.Supplier_Representative, 
                               s.Phone, s.Address, s.Email
                               FROM Suppliers s
                               JOIN Categories c ON s.CategoryId = c.Id
                               WHERE c.category_type = @category
                               ORDER BY s.Id ASC";

                    return GetDataTable(sql, new SQLiteParameter("@category", category));
            }

                // Simplified version history query
            public static DataTable GetSupplierVersions(int supplierId)
                {
                    const string sql = @"
                SELECT 
                    Name,
                    Supplier_Representative,
                    Phone,
                    Email,
                    DateChanged
                FROM SupplierHistory 
                WHERE SupplierId = @id
                ORDER BY DateChanged DESC";

                    return GetDataTable(sql, new SQLiteParameter("@id", supplierId));
            }



            public static bool UpdateSupplier(int id, string representative, string phone, string email)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Archive current record before updating
                            string archiveSql = @"
                        INSERT INTO SupplierHistory 
                            (SupplierId, Name, CategoryId, Supplier_Representative, Phone, Email, DateChanged)
                        SELECT 
                            Id, Name, CategoryId, Supplier_Representative, Phone, Email, DateChanged
                        FROM Suppliers 
                        WHERE Id = @id";

                            using (var archiveCmd = new SQLiteCommand(archiveSql, connection, transaction))
                            {
                                archiveCmd.Parameters.AddWithValue("@id", id);
                                archiveCmd.ExecuteNonQuery();
                            }

                            // Update current record (Name and CategoryId remain unchanged)
                            string updateSql = @"
                        UPDATE Suppliers 
                        SET Supplier_Representative = @rep,
                            Phone = @phone,
                            Email = @email,
                            DateChanged = CURRENT_TIMESTAMP
                        WHERE Id = @id";

                            using (var updateCmd = new SQLiteCommand(updateSql, connection, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@id", id);
                                updateCmd.Parameters.AddWithValue("@rep", representative);
                                updateCmd.Parameters.AddWithValue("@phone", phone);
                                updateCmd.Parameters.AddWithValue("@email", email);
                                updateCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Update error: {ex.Message}");
                            return false;
                        }
                    }
                }
            }





            private static DataTable GetDataTable(string sql, params SQLiteParameter[] parameters)
            {
                DataTable dt = new DataTable();

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        using (var adapter = new SQLiteDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                return dt;
            }

        private static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            int retries = 3;
            while (retries-- > 0)
            {
                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddRange(parameters);
                            return command.ExecuteNonQuery();
                        }
                    }
                }
                catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Locked)
                {
                    Thread.Sleep(100); // Wait and retry
                }
            }
            throw new Exception("Database busy");
        }


        private static object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        return command.ExecuteScalar();
                    }
                }
            }
        }
    }



