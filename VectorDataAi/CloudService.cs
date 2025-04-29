using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.VectorData;

namespace VectorDataAi;

internal class CloudService
{
    [VectorStoreRecordKey]
    public int Key { get; set; }
    [VectorStoreRecordData]
    public required string Name { get; set; }
    [VectorStoreRecordData]
    public required string Description { get; set; }
    [VectorStoreRecordVector(384, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }

}
