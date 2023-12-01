using BSES.DocumentManagementSystem.Common;
using BSES.DocumentManagementSystem.Data.Contracts;
using BSES.DocumentManagementSystem.Entities;
using BSES.DocumentManagementSystem.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BSES.DocumentManagementSystem.Data
{
    ///<inheritdoc/>
    public class DocumentDA : IDocumentEntityDA
    {
        private readonly DMSDBContext _context;

        private IDocumentEntity DocumentModelToEntity(Document document) => new DocumentEntity()
        {
            DocumentID = document.DocumentID,
            DocumentName = document.DocumentName,
            DocumentPath = document.DocumentPath,
            DocumentType = Enum.TryParse($"{document.DocumentType}", out DocumentType type) ? type : DocumentType.PDF,
            Documentversion = document.DocumentVersion,
            DocumentAccessScope = Enum.TryParse($"{document.DocumentAccessScope}", out DocumentAccessScope scope) ? scope : DocumentAccessScope.Internal,
            Category = Enum.TryParse($"{document.Category}", out DocumentCategory category) ? category : DocumentCategory.KYC,
            Year = document.Year,

            CreatedDateTime = document.CreatedDateTime,
            CreatedUserID = document.CreatedUserID,
            UpdatedUserID = document.UpdatedUserID,
            UpdatedDateTime = document.UpdatedDateTime,
            RecordStatusCode = Enum.TryParse($"{document.RecordStatusCode}", out RecordStatusCode statusCode) ? statusCode : RecordStatusCode.Active
        };

        private Document EntityToDocumentModel(IDocumentEntity documentEntity) => new Document()
        {
            DocumentID = documentEntity.DocumentID,
            DocumentName = documentEntity.DocumentName,
            DocumentPath = documentEntity.DocumentPath,
            DocumentType = (int)documentEntity.DocumentType,
            DocumentVersion = documentEntity.Documentversion,
            DocumentAccessScope = (int)documentEntity.DocumentAccessScope,
            Category = (int)documentEntity.Category,
            Year = documentEntity.Year,

            CreatedDateTime = documentEntity.CreatedDateTime,
            CreatedUserID = documentEntity.CreatedUserID,
            UpdatedUserID = documentEntity.UpdatedUserID,
            UpdatedDateTime = documentEntity.UpdatedDateTime,
            RecordStatusCode = (int)documentEntity.RecordStatusCode
        };

        public DocumentDA(DMSDBContext context) { _context = context; }

        public async Task<IDocumentEntity?> GetDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            var document = await _context.Documents.Where(x => x.DocumentID == documentID && x.RecordStatusCode == (int)RecordStatusCode.Active).FirstOrDefaultAsync(cancellationToken);
            if (document == null) return null;

            return DocumentModelToEntity(document);
        }

        public async Task<IDocumentEntity?> RemoveDocumentAsync(string documentID, CancellationToken cancellationToken)
        {
            var document = await _context.Documents.Where(x => x.DocumentID == documentID).FirstOrDefaultAsync(cancellationToken);
            if (document == null) return null;
            document.RecordStatusCode = (int)RecordStatusCode.InActive;
            document.UpdatedDateTime = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return DocumentModelToEntity(document);
        }

        public async Task<IEnumerable<IDocumentEntity>> SaveAllDocumentsAsync(IEnumerable<IDocumentEntity> documents, CancellationToken cancellationToken)
        {
            foreach (var document in documents)
            {
                var model = EntityToDocumentModel(document);
                await _context.Documents.AddAsync(model, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return documents;
        }

        public async Task<IDocumentEntity> SaveDocumentAsync(IDocumentEntity document, CancellationToken cancellationToken)
        {
            var model = EntityToDocumentModel(document);
            await _context.Documents.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return document;
        }

        public Task<IDocumentEntity> UpdateDocumentAsync(string documentID, IDocumentEntity newDocument, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
