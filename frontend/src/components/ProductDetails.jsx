export default function ProductDetails({ product, loading }) {
  if (loading) {
    return <p>Laddar produktdetaljer…</p>
  }

  if (!product) {
    return <p className="empty-state">Välj en produkt för att se detaljer.</p>
  }

  return (
    <dl className="details">
      <div><dt>Namn</dt><dd>{product.name}</dd></div>
      <div><dt>Beskrivning</dt><dd>{product.description || 'Ingen beskrivning'}</dd></div>
      <div><dt>Pris</dt><dd>{product.price.toLocaleString('sv-SE')} kr</dd></div>
      <div><dt>Lagersaldo</dt><dd>{product.stockQuantity}</dd></div>
      <div><dt>Skapad</dt><dd>{new Date(product.createdAtUtc).toLocaleString('sv-SE')}</dd></div>
      <div><dt>Senast ändrad</dt><dd>{new Date(product.updatedAtUtc).toLocaleString('sv-SE')}</dd></div>
    </dl>
  )
}
