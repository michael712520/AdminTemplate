using System.Collections.Generic;

namespace AdminTemplate.service.Dto.baseReEntity
{
    public class PairChildrenReEntity : PairReEntity
    {
        public List<PairReEntity> Children { get; set; }
    }
}
