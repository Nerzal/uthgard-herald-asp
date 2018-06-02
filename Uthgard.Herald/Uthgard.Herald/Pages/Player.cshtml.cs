using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Uthgard.Herald.Pages {
	public class PlayerModel : PageModel {
		public string Name { get; set; }
		public string Guild { get; set; }
		public string Race { get; set; }
		public string Class { get; set; }
		public string Realm { get; set; }
		public long Rp { get; set; }
		public long Xp { get; set; }
		public short Level { get; set; }
		public short RealmRank { get; set; }
		public double XpPercentOfLevel { get; set; }
		public double RpPercentOfLevel { get; set; }
		public long LastUpdated { get; set; }

		public void OnGet() {
			string searchString = "https://www2.uthgard.net/herald/api/player/Cevi";
			var result = Get(searchString);
			var player = DeserializeResult<PlayerData>(result);
		}

		private T DeserializeResult<T>(string result) {
			JsonSerializer serializer = JsonSerializer.Create();
			JsonReader reader = new JsonTextReader(new StringReader(result));
			return serializer.Deserialize<T>(reader);
		}

		//{
		//	"Name": "Cevi",
		//	"Guild": "",
		//	"Race": "Celt",
		//	"Class": "Druid",
		//	"Realm": "HIBERNIA",
		//	"Xp": 178895190569,
		//	"Rp": 1061039,
		//	"Level": 50,
		//	"RealmRank": 60,
		//	"XpPercentOfLevel": 0.01411935,
		//	"RpPercentOfLevel": 0.806624,
		//	"LastUpdated": 1527933378
		//}

	public async Task<string> GetAsync(string uri) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream)) {
				return await reader.ReadToEndAsync();
			}
		}

		public string Get(string uri) {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream)) {
				return reader.ReadToEnd();
			}
		}
	}

	public class PlayerData {
		public string Name { get; set; }
		public string Guild { get; set; }
		public string Race { get; set; }
		public string Class { get; set; }
		public string Realm { get; set; }
		public long Rp { get; set; }
		public long Xp { get; set; }
		public short Level { get; set; }
		public short RealmRank { get; set; }
		public double XpPercentOfLevel { get; set; }
		public double RpPercentOfLevel { get; set; }
		public long LastUpdated { get; set; }
	}
}