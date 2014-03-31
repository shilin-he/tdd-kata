* sql`<TableModel`>.select(x => x.prop1, x => x.prop2).generate() ==> "select col1, col2 from a_table"
* sql`<TableModel`>.select().execute() outputs an IEnumerable
* sql`<TableModel`>.select().where(predicate).order_by(x => x.name)
* Sql`<TableModel`>.update(x).generate()

* Sql.delete`<Model`>(model) ==> "delete "
