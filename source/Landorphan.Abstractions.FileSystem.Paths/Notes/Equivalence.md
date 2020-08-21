# Path Equivalence

Path's are considered equivalent if they resolve to the same point on the volume (it is assumed that they are resolved from the same starting location).  Consiquently the following are critical components of path equivalence:
+   Status
+   Anchor
+   Qualification
+   Germane Segments

## Simplification 
Path equivalence becomes most important when a path is simplified (sometimes called normalized) into a simpler form.  Simplification should only remove non germane segments from a path's segment set.  Germane segments (and germane segment groups) can not be removed via simplification.

### Simplification Steps
The following can be used to simplify a path:
+   Remove all Null ($\mathcal N$) and Empty ($\mathcal E$) segments.
+   Remove all posible Self segments ($\mathcal S$) segments.  
    >**NOTE: If a path starts with a leading self segment, that segment can not be removed**
+   Remove All posible Parent segments ($\mathcal P$) segments and their associated predicessors: 
    >NOTE: Parent segments that would take a segment backwards before a fully qualifed ($\mathtt Q$) segment are removed.

    >NOTE: Parent segments that would lead a relative path must be kept. 
    
    As a result:

    >**`C:\..`** $\Rightarrow$ **`C:\`** (because the parrent segment can not take the path before the qualified ($\mathtt Q$) root)

    >**`dir\dir\dir\..\..\..\..`** $\Rightarrow$ **`..`** (because all segments are removed by the first 3 parrent segments and only the final parent segment reamins)


