namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Utils;

public class Queries
{
    public const string CreateTables = @"
                                        BEGIN TRANSACTION;

                                        -- Table: Pokemons
                                        CREATE TABLE Pokemons (
                                            Id                INTEGER   PRIMARY KEY
                                                                        UNIQUE
                                                                        NOT NULL,
                                            Name              VARCHAR (250),
                                            ImageFront        VARCHAR (500),
                                            ImageBack         VARCHAR (500),
                                            Experience        DECIMAL
                                        );

                                        -- Table: PokemonMaster
                                        CREATE TABLE PokemonMaster (
                                            Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Name            VARCHAR (250),
                                            Age             INTEGER,
                                            CPF             VARCHAR (250)
                                        );

                                        -- Table: PokemonCaptured
                                        CREATE TABLE PokemonCaptured (
                                            Id                     INTEGER PRIMARY KEY AUTOINCREMENT,
                                            IdPokemonMaster        INTEGER,
                                            IdPokemon              INTEGER,
                                            
                                            FOREIGN KEY(IdPokemonMaster) REFERENCES PokemonMaster(Id),
                                            FOREIGN KEY(IdPokemon) REFERENCES Pokemons(Id)
                                        );
                                        COMMIT TRANSACTION;
                                    ";

    public const string InsertPokemon = @"
                                        INSERT INTO Pokemons (Id, Name, ImageFront, ImageBack, Experience) 
                                        VALUES ($id, $name, $imageFront, $imageBack, $experience)
                                    ";

    public const string InsertPokemonMaster = @"
                                        INSERT INTO PokemonMaster (Name, Age, CPF) 
                                        VALUES ($name, $age, $cPF)
                                    ";

    public const string InsertCapturePokemon = @"
                                        INSERT INTO PokemonCaptured (IdPokemonMaster, IdPokemon) 
                                        VALUES ($idPokemonMaster, $idPokemon)
                                    ";

    public const string GetPokemonByIdMaster = @"
                                        SELECT Id, [Name], Age, CPF 
                                        FROM PokemonMaster
                                        WHERE Id = id
                                    ";

    public const string ExistTables = @"
                                        SELECT name FROM sqlite_master WHERE type='table' AND name=$nomeTabela
                                    ";

    public const string PokemonMasterExist = @"
                                        SELECT 1 FROM PokemonMaster WHERE Id = @id
                                    ";


    public const string GetPokemonsCapturedByIdPokemonMaster = @"
                                        SELECT Name,
                                               ImageFront,
                                               ImageBack,
                                               Experience
                                        FROM PokemonCaptured
                                        INNER JOIN Pokemons on Pokemons.Id = PokemonCaptured.IdPokemon
                                        WHERE PokemonCaptured.IdPokemonMaster = @idPokemonMaster;
                                    ";

    public const string GetByIdPokemons = @"
                                       SELECT Id,
                                               Name,
                                               ImageFront,
                                               ImageBack,
                                               Experience
                                          FROM Pokemons
                                       WHERE Id = $id;
                                    ";
}
