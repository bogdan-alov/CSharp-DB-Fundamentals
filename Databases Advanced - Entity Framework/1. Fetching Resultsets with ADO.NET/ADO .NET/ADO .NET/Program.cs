

namespace ADO.NET
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Program
    {
        static void Main()
        {
            SqlConnection connection = new SqlConnection(


                "Server=.;" +
                "Database= MinionsDB;" +
                "Integrated Security=true");

            connection.Open();
            using (connection)
            {

            }
        }
        static void CreateDatabase(SqlConnection connection)
        {
            string query = @"CREATE DATABASE MinionsDB";
            SqlCommand command = new SqlCommand(
            query, connection);
            command.ExecuteNonQuery();
        }
        static void CreateTables(SqlConnection connection)
        {
            string query = @"
            USE MinionsDB
            CREATE TABLE Minions
            (
            MinionID int primary key identity,
            Name varchar(50),
            Age int,
            TownID int
            )

            CREATE TABLE Towns
            (
            TownID int primary key identity,
            Name varchar(25),
            CountryID int
            )

            CREATE TABLE Countries
            (
            CountryID int primary key identity,
            Name varchar(25)
            )

            create table Villains
            (
            VillainID int primary key identity,
            Name varchar(50),
            EvilnessFactor varchar(10) CHECK (EvilnessFactor IN ('Good', 'Bad', 'Evil', 'Super Evil'))
            )

            create table VillainsMinions
            (
            VillainID int not null,
            MinionID int not null,
            PRIMARY KEY (VillainID, MinionID)
            )


            ALTER TABLE Minions 
            ADD CONSTRAINT FK_Minions_Towns
            FOREIGN KEY (TownID) references Towns(TownID)

            ALTER TABLE Towns
            ADD CONSTRAINT FK_Towns_Countries
            FOREIGN KEY (CountryID) references Countries(CountryID)


            ALTER TABLE VillainsMinions
            ADD CONSTRAINT FK_VillainsMinions_Villains
            FOREIGN KEY (VillainID) REFERENCES Villains(VillainID)

            ALTER TABLE VillainsMinions
            ADD CONSTRAINT FK_VillainsMinions_Minions
            FOREIGN KEY (MinionID) REFERENCES Minions(MinionID)";
            SqlCommand command = new SqlCommand(
            query, connection);
            command.ExecuteNonQuery();
        }

        static void InsertSomeData(SqlConnection connection)
        {
            string query = @"
                INSERT INTO Countries(Name)
                VALUES ('Bulgaria'),
                ('England'),
                ('USA'),
                ('Spain')

                INSERT INTO Towns(Name, CountryID)
                VALUES ('Plovdiv', 1),
                ('Sofia', 1),
                ('Varna', 1),
                ('London', 2),
                ('New York', 3),
                ('Barcelona', 4),
                ('Madrid', 4)

                INSERT INTO Minions(Name, Age, TownID)
                VALUES ('Pesho Peshev', 25, 2),
                ('Bogdan Alov', 19, 1),
                ('Georgi Geshov', 20, 3),
                ('John Jones', 50, 4),
                ('Jose Jimenez', 35, 6),
                ('Cristiano Pedaldo', 31, 7),
                ('Leo Messi', 29, 6),
                ('Luis Suarez', 29, 5),
                ('Neymar JR', 25, 6),
                ('London Londonov', 69, 4)

                INSERT INTO Villains(Name, EvilnessFactor)
                VALUES ('SuperLoshiq Minion', 'Super Evil'),
                ('NeTolkovaLosh Minion', 'Evil'),
                ('Losh Minion', 'Bad'),
                ('Willy', 'Good')
                
                INSERT INTO VillainsMinions(VillainID, MinionID)
                VALUES  (1,3),
                        (1,2),
                        (1,4),
                        (2,5),
                        (2,7),
                        (3,3),
                        (3,2),
                        (4,1),
                        (1,7),
                        (2,1)
            ";
            SqlCommand command = new SqlCommand(
            query, connection);
            command.ExecuteNonQuery();
        }

        static void GetVillainsNames(SqlConnection connection)
        {
            string query = @"
                SELECT Name, Count(MinionID) as Servents FROM Villains v
                join VillainsMinions vm
                on v.VillainID = vm.VillainID
                GROUP BY Name
                
                ORDER BY Servents desc
            ";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            using (reader)
            {
    
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int servents = (int)reader["Servents"];

                        Console.WriteLine($"{name} {servents}");
                    }
           
                
            }
        }
        static void GetMinionsNames(SqlConnection connection, int villainId)
        {
            string villains = $@"
                SELECT Name from Villains
                where VillainID = @villainId";
            var findVillian = new SqlCommand(villains, connection);
            findVillian.Parameters.AddWithValue("@villainId", villainId);
            var reader = findVillian.ExecuteReader();
            if (reader.Read())
            {
                string villName = (string)reader["Name"];
                Console.WriteLine($"Villain: {villName}");
                var minions = $@"
                SELECT Name,Age from Minions as m
                join VillainsMinions as vm
                on m.MinionID = vm.MinionID
                where vm.VillainID = @villainId";
                var findMinions = new SqlCommand(minions, connection);
                findMinions.Parameters.AddWithValue("@villainId", villainId);
                reader.Close();
                var reader2 = findMinions.ExecuteReader();
                int i = 1;
                while (reader2.Read())
                {
                    string minionName = (string)reader2["Name"];
                    int minionAge = (int)reader2["Age"];

                    Console.WriteLine($"{i}. {minionName} {minionAge}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine($"No villain with ID {villainId} exists in the database.");
            }

        }

        static void AddMinion(SqlConnection connection, string minionName, int age, string townName, string villainName)
        {
            string query = $@"
                BEGIN TRAN
                if(@TownName NOT IN (select Name from Towns))
                begin
                insert into Towns(Name)
                values(@TownName)
                end

                if(@Villain NOT IN (SELECT Name from Villains))
                begin 
                INSERT INTO Villains(Name)
                VALUES (@Villain)
                end

                INSERT INTO Minions(Name, Age, TownID)
                VALUES (@Name, @Age, (select TownID from Towns WHERE @TownName = Name))

                INSERT INTO VillainsMinions(VillainID, MinionID)
                VALUES((select VillainID from Villains
                where @Villain = Name), (select MinionID from Minions
                where @Name = Name))

                commit";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", minionName);
            command.Parameters.AddWithValue("@Age", age);
            command.Parameters.AddWithValue("@TownName", townName);
            command.Parameters.AddWithValue("@Villain", villainName);
            var reader = command.ExecuteReader();

            using (reader)
            {

            }


        }

        static void ChangeTownNamesCasing(SqlConnection connection, string countryName)
        {
            string query = $@"
                UPDATE Towns
                SET Name = UPPER(Name)
                WHERE TownID IN (select TownID from Countries c
                join Towns t
                on t.CountryID = c.CountryID
                where c.Name = @Country)";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Country", countryName);
            var affectedRows = command.ExecuteNonQuery();
            if (affectedRows == 0)
            {
                Console.WriteLine("No town names were affected.");
            }
            else
            {
                Console.WriteLine($"{affectedRows} town names were affected.");
            }
        }

        static void RemoveVillain(SqlConnection connection, int villainId)
        {
           
            string query = $@"
                begin tran 
                delete from Villains
                where VillainID = @ID;

                ALTER TABLE VillainsMinions
                DROP CONSTRAINT FK_VillainsMinions_Villains

                alter table VillainsMinions
                ADD CONSTRAINT FK_VillainsMinions_Villains
                FOREIGN KEY(VillainID) REFERENCES Villains(VillainID) ON DELETE CASCADE

                
                commit";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", villainId);

            string query2 = $@"
                begin tran 

                ALTER TABLE VillainsMinions
                DROP CONSTRAINT FK_VillainsMinions_Villains

                alter table VillainsMinions
                ADD CONSTRAINT FK_VillainsMinions_Villains
                FOREIGN KEY(VillainID) REFERENCES Villains(VillainID) ON DELETE CASCADE

                DELETE FROM VillainsMinions
                where VillainID = @villainId 
                commit";

            string getVilainName = @"SELECT Name from Villains where VillainID = @villainID";

            var command2 = new SqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@villainId", villainId);
            var command3 = new SqlCommand(getVilainName, connection);
            command3.Parameters.AddWithValue("@villainID", villainId);
            var affectedRows = command.ExecuteNonQuery();

            int affectedMinions = command2.ExecuteNonQuery();
            var reader = command3.ExecuteReader();
           
                if (affectedRows == 0)
                {
                    Console.WriteLine("No such villain found.");
                }
                else
                {
                    Console.WriteLine($"Villain with ID {villainId} was deleted.");
                    Console.WriteLine("{0} minions were released.", affectedMinions);
                    
                }
            
              
        }

        static void IncreaseAgeWithProcedure (SqlConnection connection, int minionID)
        {
            string procedure = @"create procedure usp_GetOlder(@minionId int)
            as
            begin 

            update Minions
            set Age += 1
            where MinionID = @minionId
            end";

            string procedureQuery = @"exec usp_GetOlder @minionId";
            string queryForGettingMinions = @"select Name, Age from Minions where MinionID = @minionId";
            var command3 = new SqlCommand(procedure, connection);
            command3.Parameters.AddWithValue("@minionId", minionID);
            var command = new SqlCommand(procedureQuery, connection);
            command.Parameters.AddWithValue("@minionId", minionID);
            var command2 = new SqlCommand(queryForGettingMinions, connection);
            command2.Parameters.AddWithValue("@minionId", minionID);
            
            command.ExecuteNonQuery();

            var reader = command2.ExecuteReader();
            using (reader)
            {

                while (reader.Read())
                {
                    string name = (string)reader["Name"];
                    int age = (int)reader["Age"];

                    Console.WriteLine($"{name} {age}");
                }

            }

        }

        static void PrintAllMinionNames(SqlConnection connection)
        {
            SqlCommand minionNames = new SqlCommand("SELECT Name FROM Minions", connection);
            connection.Open();
            using (SqlDataReader reader = minionNames.ExecuteReader())
            {
                List<string> names = new List<string>();
                while (reader.Read())
                {
                    names.Add(reader["Name"].ToString());
                }

                PrintNames(names);
            }
        }
        static void PrintNames(IList<string> names)
        {
            int firstIndex = 0;
            int lastIndex = names.Count - 1;

            for (int i = 0; i < names.Count; i++)
            {
                int currentIndex;
                if (i % 2 == 0)
                {
                    currentIndex = firstIndex;
                    firstIndex++;
                }
                else
                {
                    currentIndex = lastIndex;
                    lastIndex--;
                }

                Console.WriteLine(names[currentIndex]);
            }
        }
    }
}
