using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace OpenTSDBApiWrapper {
    public interface IAnnotation {
        /// <summary>
        ///     Retrieves Annotation
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// <returns> full object with the requested changes.</returns>
        [Get("api/annotation")]
        Task<AnnotationData> GetAnnotation(AnnotationData annotationData);

        /// <summary>
        ///     Creates a new Annotation
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// <returns> full object with the requested changes.</returns>
        [Post("api/annotation")]
        Task<AnnotationData> InsertAnnotation(AnnotationData annotationData);

        /// <summary>
        ///     Updates an Annotation
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// <returns> full object with the requested changes.</returns>
        [Put("api/annotation")]
        Task<AnnotationData> UpdateAnnotation(AnnotationData annotationData);

        /// <summary>
        ///     Deletes an Annotation
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        [Delete("api/annotation")]
        Task DeleteAnnotation(AnnotationData annotationData);

        /// <summary>
        ///     Retrieves annotations in bulk
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// StartTime [Mandatory]
        /// <returns>A readonly list with the values that are available for us to do the aggregation</returns>
        [Get("api/annotation/bulk")]
        Task<IEnumerable<AnnotationData>> GetBulkAnnotations(IEnumerable<AnnotationData> annotationData);

        /// <summary>
        ///     Creates Annotations in bulk
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// <returns> full object with the requested changes.</returns>
        [Post("api/annotation/bulk")]
        Task<IEnumerable<AnnotationData>> InsertBulkAnnotation(IEnumerable<AnnotationData> annotationData);

        /// <summary>
        ///     Updates Annotations in bulk
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        /// <returns> full object with the requested changes.</returns>
        [Put("api/annotation/bulk")]
        Task<IEnumerable<AnnotationData>> UpdateAnnotation(IEnumerable<AnnotationData> annotationData);

        /// <summary>
        ///     Deletes Annotations in bulk
        /// </summary>
        /// <param name="annotationData">
        ///     All annotations are identified by the StartTime field and optionally the tsuid field.
        ///     Each note can be global, meaning it is associated with all timeseries, or it can be local, meaning it's associated
        ///     with a specific tsuid.
        ///     If the tsuid is not supplied or has an empty value, the annotation is considered to be a global note.
        /// </param>
        [Delete("api/annotation/bulk")]
        Task<AnnotationDeleteBulkResponse> DeleteAnnotation(IEnumerable<AnnotationDeleteData> annotationData);
    }
}