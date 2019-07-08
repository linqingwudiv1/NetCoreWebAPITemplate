
数据库预编译宏:
#define Q_SqlServerDB
#define Q_OracleDB
#define Q_MySqlDB
#define Q_PostgreSQLDB
#define Q_SqliteDB
#define Q_MemoryDB
修改方式：
DBAccessDLL->右键(right-click)->属性(Property)->生成()->常规()->条件编译和符号->{{写入使用数据库，如-> Q_SqliteDB}}