﻿using BSES.DocumentManagementSystem.Business.Contracts;
using BSES.DocumentManagementSystem.Entities;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BSES.DocumentManagementSystem.Controllers
{
    [Route("api/DocumentManagement")]
    [ApiController]
    [Authorize]
    public class DocumentManagementController : ControllerBase
    {
        /// <summary>
        /// Internal Logger for this class instance.
        /// </summary>
        private readonly ILogger<DocumentManagementController> _logger;

        /// <summary>
        /// Local Instance for Document Management Business Services.
        /// </summary>
        private readonly IDocumentManagementBA _documentManagementBA;

        /// <summary>
        /// Creates DocumentEntity from Model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Document Entity</returns>
        private IDocumentEntity GetDocumentEntity(DocumentModel model) => new DocumentEntity()
        {
            Category = model.DocumentCategory,
            DocumentName = $"{model.DocumentName}{Path.GetExtension(model.FileInfo!.FileName)}",
            DocumentAccessScope = model.DocumentAccessScope
        };

        /// <summary>
        /// Default Contructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="documentManagementBA"></param>
        public DocumentManagementController(ILogger<DocumentManagementController> logger, IDocumentManagementBA documentManagementBA)
        {
            _logger = logger;
            _documentManagementBA = documentManagementBA;
        }

        [HttpGet("GetDocument/{documentID}")]
        /// <summary>
        /// Asynchronously returns the document with the unique document ID.
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>File Content if successful, else the message.</returns>
        public async Task<IActionResult> GetDocumentAsync(string documentID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _documentManagementBA.GetDocumentAsync(documentID, cancellationToken);

                if (result == null || !result.IsSuccess)
                    return new OkObjectResult(result?.ErrorMessage ?? "Something went wrong while getting the document.");

                var stream = result.Value.Item2;

                return new FileStreamResult(stream, new FileExtensionContentTypeProvider().TryGetContentType(result.Value.Item1.DocumentName, out string? contentType) ? contentType : "application/octet-stream");

            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new BadRequestObjectResult($"Something went wrong while getting the document for id {documentID}");
            }
        }

        [HttpPost("SaveDocument")]
        /// <summary>
        /// Asynchronously Saves the document data stream.
        /// </summary>
        /// <param name="documentModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Unique Document ID for the saved document stream.</returns>
        public async Task<IActionResult> SaveDocumentAsync([FromForm] DocumentModel documentModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _documentManagementBA.SaveDocumentAsync(GetDocumentEntity(documentModel), documentModel.FileInfo!.OpenReadStream(), cancellationToken);
                if (result == null || !result.IsSuccess)
                    return new BadRequestObjectResult($"Something went wrong while saving the document with name {documentModel?.DocumentName ?? string.Empty}. The error is {result?.ErrorMessage ?? string.Empty}");

                return Ok(result.Value);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new BadRequestObjectResult($"Something went wrong while saving the document with name {documentModel?.DocumentName ?? string.Empty}");
            }
        }

        [HttpPost("RemoveDocument/{documentID}")]
        public async Task<IActionResult> RemoveDocumentAsync(string documentID, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _documentManagementBA.RemoveDocumentAsync(documentID, cancellationToken);

                if (result == null || !result.Value)
                    return new BadRequestObjectResult(result?.ErrorMessage ?? "Something went wrong while removing the document.");
                return new OkResult();

            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new BadRequestObjectResult($"Something went wrong while removing the document for id {documentID}");
            }
        }

        [HttpGet("ArchiveDocument/{documentID}")]
        public async Task<IActionResult> ArchiveDocumentAsync(string documentID, CancellationToken cancellationToken = default)
        {
            try
            {
                //TODO: this is tempoerary and will be changed.
                var result = await _documentManagementBA.GetDocumentAsync(documentID, cancellationToken);

                if (result == null || !result.IsSuccess)
                    return new OkObjectResult(result?.ErrorMessage ?? "Something went wrong while getting the document.");

                var archiveResult = await _documentManagementBA.MarkDocumentArchiveAsync(result.Value.Item1, "BRPL", cancellationToken);

                if (archiveResult == null || !archiveResult.Value)
                    return new BadRequestObjectResult(result?.ErrorMessage ?? "Something went wrong while archiving the document.");

                return new OkObjectResult($"Document had been archived.");
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new BadRequestObjectResult($"Something went wrong while archiving the document for id {documentID}");
            }
        }
    }
}
