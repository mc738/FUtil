module FUtil.Maps

/// Join to maps.
/// From: https://stackoverflow.com/questions/3974758/in-f-how-do-you-merge-2-collections-map-instances.
let join (p: Map<'a, 'b>) (q: Map<'a, 'b>) =
    Map
        (Seq.concat [ (Map.toSeq p)
                      (Map.toSeq q) ])

