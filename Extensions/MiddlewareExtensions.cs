using TaskTrackerAPI.Middlewares;

namespace TaskTrackerAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
