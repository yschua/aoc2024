class Day09_2
{
    record struct Block(int Id, int Size);

    static void Main(string[] args)
    {
        var line = File.ReadAllLines(args[0])[0];
        var blocks = new LinkedList<Block>();
        foreach (var i in Enumerable.Range(0, line.Length))
        {
            var id = (i % 2 == 0) ? i / 2 : -1;
            var size = line[i] - '0';
            if (size > 0)
            {
                blocks.AddLast(new Block(id, size));
            }
        }

        LinkedListNode<Block> FindFile(int id)
        {
            for (var node = blocks.Last; node != null; node = node.Previous)
            {
                if (node.Value.Id == id)
                {
                    return node;
                }
            }
            throw new InvalidOperationException();
        }

        LinkedListNode<Block>? FindFree(LinkedListNode<Block> nodeToMove)
        {
            for (var node = blocks.First; node != nodeToMove; node = node.Next)
            {
                if (node.Value.Id == -1 && node.Value.Size >= nodeToMove.Value.Size)
                {
                    return node;
                }
            }
            return null;
        }

        void MergeFreeNodes(LinkedListNode<Block> a, LinkedListNode<Block> b)
        {
            if (a == null || b == null || a.Value.Id != -1 || b.Value.Id != -1)
            {
                return;
            }
            if (a.Next != b)
            {
                throw new InvalidOperationException();
            }
            a.ValueRef.Size += b.Value.Size;
            blocks.Remove(b);
        }

        foreach (var id in Enumerable.Range(0, blocks.Last().Id + 1).Reverse())
        {
            var nodeToMove = FindFile(id);
            var nodeToFill = FindFree(nodeToMove);
            if (nodeToFill == null)
            {
                continue;
            }

            var freeSize = nodeToFill.Value.Size;
            nodeToFill.Value = nodeToMove.Value;
            var padding = freeSize - nodeToMove.Value.Size;
            if (padding > 0)
            {
                blocks.AddAfter(nodeToFill, new Block(-1, padding));
            }

            nodeToMove.ValueRef.Id = -1;
            MergeFreeNodes(nodeToMove, nodeToMove.Next);
            MergeFreeNodes(nodeToMove.Previous, nodeToMove);
        }

        long checksum = 0;
        int position = 0;
        foreach (var block in blocks)
        {
            foreach (var _ in Enumerable.Range(0, block.Size))
            {
                if (block.Id != -1)
                {
                    checksum += position * block.Id;
                }
                position++;
            }
        }
        Console.WriteLine(checksum);
    }
}