
using Dapper;
using GitHub.Data.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Z.Dapper.Plus;
using static GitHub.Data.Model.GitHubRepositoryData;

namespace GitHub.Data
{
    public class GitHubRepository : IGitHubRepository
    {
        private DbConnection Dbconnection;
        private readonly string strConnection;
        private readonly string strInsertFamousRepositories;

        public GitHubRepository(IConfiguration configuration)
        {
            strConnection = configuration["ConnectionStrings:DefaultConnection"];
            strInsertFamousRepositories = configuration["SQLscripts:InsertFamousRepositories"];

            DBConnect();
        }

        public async Task<bool> SaveFamousRepositories(List<RepositoryData> gitHubRepositoriesData)
        {
            try
            {
                await InsertRepositoryData(gitHubRepositoriesData);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void DBConnect()
        {
            if (Dbconnection == null)
                Dbconnection = new SqlConnection(strConnection);
        }

        private async Task InsertRepositoryData(List<RepositoryData> gitHubRepositoriesData)
        {
            foreach(var repositoryData in gitHubRepositoriesData)
            { 
                var insertCommand = getInsertString(repositoryData);

                executeInsert(insertCommand);
            };
        }

        private string getInsertString(RepositoryData repositoryData)
        {
            var strInsertFamousRepositories = @"INSERT INTO dbo.FamousRepositoryData
                                        VALUES
                                       (@id,'@name','@full_name','@language'
                                       ,'@owner_login',@owner_id
                                       ,'@owner_url','@html_url'
                                       ,'@description','@repository_url','@collaborators_url'
                                       ,'@commits_url',CAST('@created_at' AS DATE)
                                       ,CAST('@updated_at' AS DATE),CAST('@pushed_at' AS DATE)
                                       ,'@clone_url',@stargazers_count
                                       ,@watchers_count,@has_wiki
                                       ,'@license_key','@license_name')";

            var strInsertBuilder = new StringBuilder(strInsertFamousRepositories);

            strInsertBuilder.Replace("@id", repositoryData.id.ToString());
            strInsertBuilder.Replace("@name", repositoryData.name);
            strInsertBuilder.Replace("@full_name", repositoryData.full_name);
            strInsertBuilder.Replace("@owner_login", repositoryData.owner.login);
            strInsertBuilder.Replace("@owner_id", repositoryData.owner.id.ToString());
            strInsertBuilder.Replace("@owner_url", repositoryData.owner.url);
            strInsertBuilder.Replace("@html_url", repositoryData.html_url);
            strInsertBuilder.Replace("@description", repositoryData.description);
            strInsertBuilder.Replace("@repository_url", repositoryData.url);
            strInsertBuilder.Replace("@collaborators_url", repositoryData.collaborators_url);
            strInsertBuilder.Replace("@commits_url", repositoryData.commits_url);
            strInsertBuilder.Replace("@created_at", repositoryData.created_at.ToString("yyyy-MM-dd"));
            strInsertBuilder.Replace("@updated_at", repositoryData.updated_at.ToString("yyyy-MM-dd"));
            strInsertBuilder.Replace("@pushed_at", repositoryData.pushed_at.ToString("yyyy-MM-dd"));
            strInsertBuilder.Replace("@clone_url", repositoryData.clone_url);
            strInsertBuilder.Replace("@stargazers_count", repositoryData.stargazers_count.ToString());
            strInsertBuilder.Replace("@watchers_count", repositoryData.watchers_count.ToString());
            strInsertBuilder.Replace("@has_wiki", Convert.ToInt32(repositoryData.has_wiki).ToString());
            strInsertBuilder.Replace("@license_key", repositoryData.license == null ? "" : repositoryData.license.key);
            strInsertBuilder.Replace("@license_name", repositoryData.license == null ? "" : repositoryData.license.name);
            strInsertBuilder.Replace("@language", repositoryData.language);

            return strInsertBuilder.ToString();
        }

        private void executeInsert(string InsertCommand)
        {
            DBConnect();

            Dbconnection.Execute(InsertCommand);
            }

        public async Task CleanDatabaseFromLanguages(IEnumerable<string> ListOfLanguages)
        {
            var strDeleteLanguagesInDB = getStrDeleteLanguafesInDB(ListOfLanguages);

            Dbconnection.Execute(strDeleteLanguagesInDB);
        }

        private string getStrDeleteLanguafesInDB(IEnumerable<string> ListOfLanguages)
        {
            var LanguagesWhereInClause = string.Join(",",ListOfLanguages.ToList());

            string LanguagesWhereInClauseAdjusted = "'" + LanguagesWhereInClause.Replace(",", "','") + "'";

            var deleteCommand = $"Delete from dbo.FamousRepositoryData where language in ({LanguagesWhereInClauseAdjusted})";

            return deleteCommand;
        }
    }
}