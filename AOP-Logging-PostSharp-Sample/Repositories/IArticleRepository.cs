using AOP_Logging_PostSharp_Sample.Common.Models;

namespace AOP_Logging_PostSharp_Sample.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetArticles();

        Article GetArticle(int id);
        Article AddArticle(Article article);
    }
}
