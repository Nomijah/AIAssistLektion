export default function ProductList({ products, selectedId, onSelect, onDelete }) {
  if (products.length === 0) {
    return <p className="empty-state">Det finns inga produkter ännu.</p>
  }

  return (
    <ul className="product-list">
      {products.map((product) => (
        <li className={product.id === selectedId ? 'selected' : ''} key={product.id}>
          <button className="product-summary" type="button" onClick={() => onSelect(product.id)}>
            <strong>{product.name}</strong>
            <span>{product.price.toLocaleString('sv-SE')} kr</span>
            <span>{product.stockQuantity} i lager</span>
          </button>
          <button
            className="danger-button"
            type="button"
            onClick={() => onDelete(product)}
            aria-label={`Ta bort ${product.name}`}
          >
            Ta bort
          </button>
        </li>
      ))}
    </ul>
  )
}
