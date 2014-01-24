using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Glib.Exceptions;

namespace Glib.SQL
{
    /// <summary>
    /// The <see cref="Glib.SQL"/> namespace provides various SQL tools.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// Various SQL-related extensions.
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Check if the specified object is either null or DBNull.
        /// </summary>
        /// <param name="nullCheck">The object to check against null and DBNull.</param>
        /// <returns>Whether or not the specified object is null or DBNull.</returns>
        public static bool IsNull(this object nullCheck)
        {
            return nullCheck == DBNull.Value || nullCheck == null;
        }

        /// <summary>
        /// Open the specified SqlConnection if it is not opened already.
        /// </summary>
        /// <param name="conn">The connection to open.</param>
        public static void OpenIfNeeded(this System.Data.SqlClient.SqlConnection conn)
        {
            if (conn.IsNull())
            {
                throw new ArgumentNullException("conn");
            }
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }
        
        /// <summary>
        /// Get the SQL parameter represented by the specified ISQLParameterProvider.
        /// </summary>
        /// <param name="provider">The ISQLParameterProvider to get the parameter data from.</param>
        /// <returns>A SqlParameter representing the information stored by the ISQLParameterProvider.</returns>
        public static SqlParameter GetParameter(this ISQLParameterProvider provider)
        {
            return new SqlParameter(provider.ParameterName, provider.ParameterValue);
        }
    }

    /// <summary>
    /// An interface representing a provider of SQL parameter information.
    /// </summary>
    public interface ISQLParameterProvider
    {
        /// <summary>
        /// Gets the name of the provided parameter.
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Gets the value of the provided parameter.
        /// </summary>
        object ParameterValue { get; set; }
    }

    /// <summary>
    /// A class representing a SQL stored procedure.
    /// </summary>
    [Obsolete()]
    public class StoredProcedure
    {
        private SqlConnection _connection;

        /// <summary>
        /// The SqlConnection used to connect to the database.
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                _underlyingCommand.Connection = _connection;
            }
        }

        private SqlCommand _underlyingCommand;

        /// <summary>
        /// Get the collection of <seealso cref="SqlParameter"/>s associated with this command.
        /// </summary>
        public SqlParameterCollection Parameters
        {
            get
            {
                return _underlyingCommand.Parameters;
            }
        }

        /// <summary>
        /// Add a SqlParameter with the specified name and value to the collection of parameters.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="value">The value of the parameter.</param>
        public void AddParameter(string name, object value)
        {
            Parameters.AddWithValue(name, value);
        }

        /// <summary>
        /// Add the specified SqlParameters to the parameters of the underlying command.
        /// </summary>
        /// <param name="parameters">The parameters to add.</param>
        public void AddParameters(params SqlParameter[] parameters)
        {
            _underlyingCommand.Parameters.AddRange(parameters);
        }

        /// <summary>
        /// Remove the SqlParameter with the specified name.
        /// </summary>
        /// <param name="name">The name of the SqlParameter to remove.</param>
        /// <returns>Whether or not the command was successfully removed.</returns>
        public bool RemoveParameter(string name)
        {
            SqlParameter rm = null;
            foreach (SqlParameter param in Parameters)
            {
                if (param.ParameterName == name)
                {
                    rm = param;
                }
            }
            if (rm != null)
            {
                Parameters.Remove(rm);
                return true;
            }
            return false;


        }

        /// <summary>
        /// The underlying stored procedure executed against the database.
        /// </summary>
        /// <remarks>
        /// Cannot be set to a non stored procedure command.
        /// </remarks>
        public SqlCommand Command
        {
            get { return _underlyingCommand; }
            set { value.CommandType = CommandType.StoredProcedure; _underlyingCommand = value; }
        }


        /// <summary>
        /// Create a new StoredProcedure.
        /// </summary>
        /// <param name="connection">The SqlConnection used to connect to the database.</param>
        /// <param name="procName">The name of the stored procedure to execute.</param>
        public StoredProcedure(SqlConnection connection, string procName)
        {
            _underlyingCommand = new SqlCommand(procName, connection);
            _underlyingCommand.CommandType = CommandType.StoredProcedure;
            _connection = connection;
        }

        /// <summary>
        /// Create a new StoredProcedure.
        /// </summary>
        /// <param name="connectionString">The connecting string used to connect to the database.</param>
        /// <param name="procName">The name of the stored procedure to execute.</param>
        public StoredProcedure(string connectionString, string procName)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            _underlyingCommand = new SqlCommand(procName, conn);
            _underlyingCommand.CommandType = CommandType.StoredProcedure;
            _connection = conn;
        }

        /// <summary>
        /// A destructor closing all known open SqlDataReaders associated with this command.
        /// </summary>
        ~StoredProcedure()
        {
            foreach (SqlDataReader reader in _dataReaderReferences)
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        private List<SqlDataReader> _dataReaderReferences = new List<SqlDataReader>();

        /// <summary>
        /// An event called after the completion of the underlying command.
        /// </summary>
        public event StatementCompletedEventHandler CommandCompleted
        {
            add
            {
                _underlyingCommand.StatementCompleted += value;
            }
            remove
            {
                _underlyingCommand.StatementCompleted -= value;
            }
        }

        /// <summary>
        /// Execute this StoredProcedure, and return the results as a SqlDataReader.
        /// </summary>
        /// <remarks>
        /// Must be closed after use.
        /// </remarks>
        /// <returns>A SqlDataReader representing the results of this command.</returns>
        public SqlDataReader ExecuteDataReader()
        {
            openConnIfNeeded();
            SqlDataReader reader = _underlyingCommand.ExecuteReader();
            _dataReaderReferences.Add(reader);
            return reader;
        }

        /// <summary>
        /// Executes this StoredProcedure, and return the results as a scalar value (int).
        /// </summary>
        /// <returns>The results of this command as a scalar value (int).</returns>
        public int ExecuteScalar()
        {
            openConnIfNeeded();
            return (int)_underlyingCommand.ExecuteScalar();
        }

        private void openConnIfNeeded()
        {
            Connection.OpenIfNeeded();
        }

        /// <summary>
        /// Executes this StoredProcedure, and return the number of rows affected.
        /// </summary>
        /// <returns>The number of rows affected by this command.</returns>
        public int Execute()
        {
            openConnIfNeeded();
            return _underlyingCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute this StoredProcedure, and return the results as a DataTable.
        /// </summary>
        /// <returns>A DataTable representing the results of this command.</returns>
        public DataTable ExecuteDataTable()
        {
            openConnIfNeeded();
            SqlDataAdapter data = new SqlDataAdapter(_underlyingCommand);
            DataTable res = new DataTable();
            data.Fill(res);
            return res;
        }
    }

    /// <summary>
    /// A wrapper class around a SqlCommand, defaulting to a stored procedure.
    /// </summary>
    [Obsolete()]
    public class CommandWrapper
    {
        /// <summary>
        /// The stored procedure this class is wrapping.
        /// </summary>
        public SqlCommand Command = null;

        private List<SqlDataReader> _openReaders = new List<SqlDataReader>();

        /// <summary>
        /// Get the paramaters passed with this SqlCommand.
        /// </summary>
        public SqlParameterCollection Paramaters
        {
            get
            {
                return Command.Parameters;
            }
        }

        /// <summary>
        /// A destructor closing all open SqlDataReaders.
        /// </summary>
        ~CommandWrapper()
        {
            foreach (SqlDataReader d in _openReaders)
            {
                if (!d.IsClosed)
                {
                    d.Close();
                }
            }
        }

        /// <summary>
        /// Add a SqlParamater to the command's arguments.
        /// </summary>
        /// <param name="param">The SqlParamater to add.</param>
        public void AddParamater(SqlParameter param)
        {
            Command.Parameters.Add(param);
        }

        /// <summary>
        /// Adds multiple SqlParamaters to the command's arguments.
        /// </summary>
        /// <param name="paramaters">The SqlParamaters to add.</param>
        public void AddParamaters(params SqlParameter[] paramaters)
        {
            Command.Parameters.AddRange(paramaters);
        }

        /// <summary>
        /// Initialize a new StoredProcedureWrapper with the specified connection and stored procedure.
        /// </summary>
        /// <param name="connect">The SqlConnection to connect to the database with.</param>
        /// <param name="storedProcName">The name of the stored procedure to use.</param>
        public CommandWrapper(SqlConnection connect, String storedProcName)
        {
            Command = new SqlCommand(storedProcName, connect);
            Command.CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// HIGHLY DISRECOMMENDED UNLESS COMMAND HAS NO USER INPUT, FOR SECURITY'S SAKE.
        /// Change the command type to a regular SQL statement.
        /// Vulnerable to SQL injection attacks.
        /// </summary>
        public void ChangeToStandardSqlCommand()
        {
            Command.CommandType = CommandType.Text;
        }

        /// <summary>
        /// Append untrusted user input to the existing SQL command text.
        /// Should not be relied on for security of a SQL command.
        /// Does not escape text.
        /// Throws a SecurityException in case of detected suspicious character.
        /// </summary>
        /// <exception cref="System.Exception">Thrown if a disallowed sharacter sequence is detected.</exception>
        /// <param name="input">The user input to append to the SQL command.</param>
        /// <param name="allowQuotes">Whether or not to allow quotes in the user input string.</param>
        /// <param name="allowOr">Whether or not to allow the word "OR" in the user input string.</param>
        public void AppendUntrustedInputToCommand(string input, bool allowQuotes, bool allowOr)
        {
            if ((input.Contains(@"'") || input.Contains("\"")) && !allowQuotes)
            {
                //throw new Exception("User input string contains disallowed quotes.");
                throw new SecurityException(SecurityRiskType.SQLInject);
            }
            if ((input.ToUpper().Contains(@"OR")) && !allowOr)
            {
                //throw new Exception("User input string contains disallowed SQL keyword OR.");
                throw new SecurityException(SecurityRiskType.SQLInject);
            }
            
            Command.CommandText += input;
        }

        /// <summary>
        /// Initialize a new StoredProcedureWrapper with the specified connection and stored procedure.
        /// </summary>
        /// <param name="connectStr">The connection string to use to connect to the database.</param>
        /// <param name="storedProcName">The name of the stored procedure to use.</param>
        public CommandWrapper(String connectStr, String storedProcName)
            : this(new SqlConnection(connectStr), storedProcName)
        {
        }

        /// <summary>
        /// Execute this command as a SqlDataReader.
        /// It is important to close this data reader after use.
        /// Thedestructor's automatic closing of all open SqlDataReaders should not be relied on.
        /// </summary>
        /// <returns>A SqlDataReader containing the results of the command.</returns>
        public SqlDataReader ExecuteReader()
        {
            SqlDataReader returnValue = Command.ExecuteReader();
            _openReaders.Add(returnValue);
            return returnValue;
        }

        /// <summary>
        /// Execute this SqlCommand, and put the results in a DataTable.
        /// </summary>
        /// <returns>A DataTable containing the results of the command.</returns>
        public DataTable ExecuteDataTable()
        {
            SqlDataAdapter adapt = new SqlDataAdapter(Command);
            DataTable info = new DataTable();
            adapt.Fill(info);
            return info;
        }

        /// <summary>
        /// Execute this command without returning any output.
        /// </summary>
        public void Execute()
        {
            Command.ExecuteReader().Close();
        }

        /// <summary>
        /// Initialize a new StoredProcedureWrapper with the specified connection, stored procedure, and paramaters.
        /// </summary>
        /// <param name="connect">The SqlConnection to connect to the database with.</param>
        /// <param name="storedProcName">The name of the stored procedure to use.</param>
        /// <param name="paramaters">The SqlParamaters to add to this SqlCommand.</param>
        public CommandWrapper(SqlConnection connect, String storedProcName, params SqlParameter[] paramaters)
            : this(connect, storedProcName)
        {
            Command.Parameters.AddRange(paramaters);
        }
    }
}
