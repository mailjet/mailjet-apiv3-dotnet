using Mailjet.Repositories;
using Mailjet.Repositories.Interfaces;
using MailJet.CoreDemoWebApp.Models.Configuration;

namespace MailJet.CoreDemoWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            //START Add MailJet
            builder.Services.Configure<MailJetConfigurationModel>(builder.Configuration.GetSection("MailJetConfiguration"));
            RegisterMailJetRepositories(builder.Services);
            //END Add MailJet

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }

        public static void RegisterMailJetRepositories(IServiceCollection services)
        {
            services.AddSingleton<ICampaigndraftRepository, CampaigndraftMailJetRepository>();
            services.AddSingleton<ICampaignRepository, CampaignMailJetRepository>();
            services.AddSingleton<IContactdataRepository, ContactdataMailJetRepository>();
            services.AddSingleton<IContactRepository, ContactMailJetRepository>();
            services.AddSingleton<IContactmetadataRepository, ContactmetadataMailJetRepository>();
            services.AddSingleton<IContactslistRepository, ContactslistMailJetRepository>();
            services.AddSingleton<IJobRepository, JobMailJetRepository>();
            services.AddSingleton<ITemplateDetailcontentRepository, TemplateDetailcontentMailJetRepository>();
            services.AddSingleton<ITemplateRepository, TemplateMailJetRepository>();
        }
    }
}