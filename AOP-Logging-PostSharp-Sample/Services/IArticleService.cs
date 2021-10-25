using AOP_Logging_PostSharp_Sample.Common.Models;

namespace AOP_Logging_PostSharp_Sample.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticles();

        Article GetArticle(int id);

        Article AddArticle(Article article);
    }
}
