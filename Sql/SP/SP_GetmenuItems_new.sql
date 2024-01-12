CREATE OR ALTER PROCEDURE SP_GetmenuItems_new 
    --, @JSONResult NVARCHAR(MAX) OUTPUT
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

   SELECT (
   SELECT 
        c.category_id,
        c.category_name,
        (
            SELECT 
                i.item_id,
                i.item_name AS 'ItemType',
                (
                    SELECT 
                        si.subitem_id,
                        si.subitem_name,
                        si.available
                    FROM 
                        subitems si
                    WHERE 
                        si.item_id = i.item_id
                        AND si.available = 1
                    FOR JSON PATH
                ) AS 'Items'
            FROM 
                items i
            INNER JOIN 
                Menu M ON i.item_id = m.item_id
            WHERE 
                m.category_id = c.category_id
                AND c.available = 1
            FOR JSON PATH
        ) AS 'MenuItems'
    FROM 
        Categories c
    WHERE 
        c.available = 1
    FOR JSON PATH
	) as JsonResult
END

--Exec SP_GetmenuItems_new 1
