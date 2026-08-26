import { useState } from 'react'

const initialForm = { name: '', description: '', price: '', stockQuantity: '' }

export default function CreateProductForm({ onCreate, submitting }) {
  const [form, setForm] = useState(initialForm)
  const [validationError, setValidationError] = useState('')

  function updateField(event) {
    setForm({ ...form, [event.target.name]: event.target.value })
  }

  async function handleSubmit(event) {
    event.preventDefault()

    if (!form.name.trim()) {
      setValidationError('Namn krävs.')
      return
    }

    const price = Number(form.price)
    const stockQuantity = Number(form.stockQuantity)
    if (price < 0 || stockQuantity < 0) {
      setValidationError('Pris och lagersaldo får inte vara negativa.')
      return
    }

    setValidationError('')
    const created = await onCreate({
      name: form.name.trim(),
      description: form.description.trim() || null,
      price,
      stockQuantity,
    })

    if (created) {
      setForm(initialForm)
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Namn
        <input name="name" maxLength="120" value={form.name} onChange={updateField} required />
      </label>
      <label>
        Beskrivning
        <textarea name="description" maxLength="1000" value={form.description} onChange={updateField} />
      </label>
      <div className="form-row">
        <label>
          Pris
          <input name="price" type="number" min="0" step="0.01" value={form.price} onChange={updateField} required />
        </label>
        <label>
          Lagersaldo
          <input name="stockQuantity" type="number" min="0" step="1" value={form.stockQuantity} onChange={updateField} required />
        </label>
      </div>
      {validationError && <p className="message error" role="alert">{validationError}</p>}
      <button className="primary-button" type="submit" disabled={submitting}>
        {submitting ? 'Sparar…' : 'Lägg till produkt'}
      </button>
    </form>
  )
}
