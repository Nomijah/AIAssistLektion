import { useCallback, useEffect, useState } from 'react'
import { createProduct, deleteProduct, getProduct, getProducts } from './api/productsApi.js'
import CreateProductForm from './components/CreateProductForm.jsx'
import ProductDetails from './components/ProductDetails.jsx'
import ProductList from './components/ProductList.jsx'

export default function App() {
  const [products, setProducts] = useState([])
  const [selectedProduct, setSelectedProduct] = useState(null)
  const [loading, setLoading] = useState(true)
  const [detailsLoading, setDetailsLoading] = useState(false)
  const [submitting, setSubmitting] = useState(false)
  const [message, setMessage] = useState(null)

  const loadProducts = useCallback(async () => {
    setLoading(true)
    try {
      setProducts(await getProducts())
    } catch (error) {
      setMessage({ type: 'error', text: error.message })
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    loadProducts()
  }, [loadProducts])

  async function selectProduct(id) {
    setDetailsLoading(true)
    setMessage(null)
    try {
      setSelectedProduct(await getProduct(id))
    } catch (error) {
      setMessage({ type: 'error', text: error.message })
    } finally {
      setDetailsLoading(false)
    }
  }

  async function handleCreate(product) {
    setSubmitting(true)
    setMessage(null)
    try {
      const created = await createProduct(product)
      setProducts((current) => [...current, created].sort((a, b) => a.name.localeCompare(b.name)))
      setSelectedProduct(created)
      setMessage({ type: 'success', text: `${created.name} skapades.` })
      return true
    } catch (error) {
      setMessage({ type: 'error', text: error.message })
      return false
    } finally {
      setSubmitting(false)
    }
  }

  async function handleDelete(product) {
    if (!window.confirm(`Vill du ta bort ${product.name}?`)) {
      return
    }

    setMessage(null)
    try {
      await deleteProduct(product.id)
      setProducts((current) => current.filter((item) => item.id !== product.id))
      if (selectedProduct?.id === product.id) {
        setSelectedProduct(null)
      }
      setMessage({ type: 'success', text: `${product.name} togs bort.` })
    } catch (error) {
      setMessage({ type: 'error', text: error.message })
    }
  }

  return (
    <main>
      <header>
        <p className="eyebrow">Undervisningsprojekt</p>
        <h1>Product Inventory</h1>
        <p>En enkel översikt över produkter och lagersaldo.</p>
      </header>

      {message && <p className={`message ${message.type}`} role="status">{message.text}</p>}

      <section className="layout">
        <article className="card inventory-card">
          <h2>Produkter</h2>
          {loading ? <p>Laddar produkter…</p> : (
            <ProductList
              products={products}
              selectedId={selectedProduct?.id}
              onSelect={selectProduct}
              onDelete={handleDelete}
            />
          )}
        </article>

        <aside className="side-column">
          <article className="card">
            <h2>Produktdetaljer</h2>
            <ProductDetails product={selectedProduct} loading={detailsLoading} />
          </article>
          <article className="card">
            <h2>Ny produkt</h2>
            <CreateProductForm onCreate={handleCreate} submitting={submitting} />
          </article>
        </aside>
      </section>
    </main>
  )
}
