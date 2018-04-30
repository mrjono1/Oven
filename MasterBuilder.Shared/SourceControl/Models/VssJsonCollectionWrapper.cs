namespace MasterBuilder.SourceControl.Models
{
    /// <summary>
    /// Vss Json Collection Wrapper
    /// </summary>
    public class VssJsonCollectionWrapper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VssJsonCollectionWrapper() { }

        /// <summary>
        /// Value
        /// </summary>
        public GetRepository[] Value { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }
    }
}
