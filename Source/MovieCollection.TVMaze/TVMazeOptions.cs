using System.Net.Http.Headers;

namespace MovieCollection.TVMaze
{
    /// <summary>
    /// The <c>TVMazeOptions</c> class.
    /// </summary>
    public class TVMazeOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeOptions"/> class.
        /// </summary>
        public TVMazeOptions()
            : base()
        {
            ApiAddress = "http://api.tvmaze.com";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TVMazeOptions"/> class.
        /// </summary>
        /// <param name="apiKey">the api key.</param>
        public TVMazeOptions(string apiKey)
            : this()
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or sets base address to bypass restrictions if necessary.
        /// </summary>
        public string ApiAddress { get; set; }

        /// <summary>
        /// Gets or sets apiKey.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the name (and version) of the product using this library.
        /// </summary>
        /// <remarks>
        /// This overrides the <see cref="System.Net.Http.HttpClient.DefaultRequestHeaders"/>.
        /// </remarks>
        public ProductHeaderValue ProductInformation { get; set; }
    }
}
