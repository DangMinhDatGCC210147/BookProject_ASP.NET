using BusinessObjects;

namespace DatAccess
{
	public class LanguageDAO
	{
			public static List<Language> GetLanguages()
			{
				var listLanguages = new List<Language>();
				try
				{
					using (var context = new ApplicationDBContext())
					{
						listLanguages = context.Languages.ToList();
					}

				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
				return listLanguages;
			}

		public static Language FindLanguageById(int id)
		{
			var language = new Language();
			try
			{
				using (var context = new ApplicationDBContext())
				{
					language = context.Languages.Find(id);
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return language;
		}

		public static void SaveLanguage(Language language)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Languages.Add(language);
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
			}
		}

		public static void UpdateLanguage(Language language)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Entry<Language>(language).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public static void DeleteLanguage(Language language)
		{
			try
			{
				using (var context = new ApplicationDBContext())
				{
					context.Languages.Remove(FindLanguageById(language.Id));
					context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}