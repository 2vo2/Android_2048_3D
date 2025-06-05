namespace Cube.Merger
{
    public class UniversalMerger : CubeMerger
    {
        public override void MergeCube(CubeUnit self, CubeUnit other)
        {
            EnableMergeCube(self, false);

            AddMergeValueToScore(other);

            TossMergeCube(other);
            
            other.CubeMerger.InvokeCubeMerged(other.CubeNumber * 2);
        }
    }
}