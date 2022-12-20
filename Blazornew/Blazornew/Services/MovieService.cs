using Blazornew.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazornew.Services;

public class MovieService
{
    protected readonly ApplicationContext dbContext;

    public Movie GetMovieFromTitle(string inputValue)
    {
        var findedMovies = dbContext.MovieDBs.Include(a => a.top10).Include(a => a.Tags).Include(a => a.Actors)
            .Where(m => m.Name.Contains(inputValue)).ToList().FirstOrDefault();
        return findedMovies;
    }    
    public MovieService(ApplicationContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public ActorsDB GetActorFromTitle(string inputValue)
    {
        return  dbContext.ActorsDBs
            .Include(a => a.movie)
            .Where(a => a.name.ToLower() == inputValue.ToLower()).FirstOrDefault();
    }
    public List<Movie> GetMoviesFromTag(string inputValue)
    {
        return dbContext.MovieDBs.Include(m => m.Tags)
            .Where(m => m.Tags.Any(n => n.name.ToLower() == inputValue.ToLower())).ToList(); 
        //dbContext.TagsDbs.Include(t => t.movie).Where(t => t.name.ToLower() == inputValue.ToLower()).First().movie.ToList();
    }
    public List<Movie> GetMoviesFromActor(string inputValue)
    {
        //return dbContext.ActorsDBs.Include(a => a.movie).Where(a => a.name.ToLower() == inputValue.ToLower()).First().movie.ToList();
         return  dbContext.MovieDBs.Include(m => m.Actors)
            .Where(m => m.Actors.Any(n => n.name.ToLower() == inputValue.ToLower())).ToList();
    }

}