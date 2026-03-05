// ===== COMMON.JS =====
// Client-side utilities for The Weeb Den Shop (ASP.NET Razor Pages)
// Cart management is handled server-side via session.

// ===== NOTIFICATIONS =====
function showNotification(message, type = 'success') {
  let existing = document.querySelector('.custom-notification')
  if (existing) {
    existing.remove()
  }

  let notification = document.createElement('div')
  notification.className = 'custom-notification'

  let icon =
    type === 'success'
      ? 'fa-check-circle'
      : type === 'error'
      ? 'fa-exclamation-circle'
      : 'fa-info-circle'

  let bgGradient =
    type === 'success'
      ? 'linear-gradient(135deg, #10b981, #059669)'
      : type === 'error'
      ? 'linear-gradient(135deg, #ef4444, #dc2626)'
      : 'linear-gradient(135deg, #f59e0b, #d97706)'

  const width = window.innerWidth
  let topPos = '90px'
  let rightPos = '20px'
  let minWidth = '300px'

  if (width <= 390) {
    rightPos = '5px'
    minWidth = '280px'
  } else if (width <= 520) {
    rightPos = '10px'
    topPos = '80px'
    minWidth = '290px'
  }

  notification.style.cssText = `
    position: fixed;
    top: ${topPos};
    right: ${rightPos};
    z-index: 9999;
  `

  notification.innerHTML = `
    <div style="
      background: ${bgGradient};
      color: white;
      padding: 1rem 1.5rem;
      border-radius: 12px;
      box-shadow: 0 8px 24px rgba(0,0,0,0.2), 0 4px 8px rgba(0,0,0,0.1);
      display: flex;
      align-items: center;
      gap: 0.75rem;
      font-size: 1rem;
      font-weight: 500;
      min-width: ${minWidth};
      max-width: calc(100vw - ${rightPos} - ${rightPos});
      animation: notificationSlideIn 0.4s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
    ">
      <i class="fas ${icon}" style="
        font-size: 1.5rem;
        animation: notificationIconPop 0.6s cubic-bezier(0.34, 1.56, 0.64, 1) 0.2s forwards;
        flex-shrink: 0;
      "></i>
      <span style="word-break: break-word;">${message}</span>
    </div>
  `

  document.body.appendChild(notification)

  setTimeout(() => {
    notification.style.animation =
      'notificationSlideOut 0.4s cubic-bezier(0.6, -0.28, 0.74, 0.05) forwards'
    setTimeout(() => notification.remove(), 400)
  }, 3000)
}

// ===== UTILITY FUNCTIONS =====

function formatPrice(price) {
  return (
    '₱' +
    price.toLocaleString('en-PH', {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    })
  )
}

function generateStars(rating) {
  let stars = ''
  let fullStars = Math.floor(rating)
  let hasHalf = rating % 1 >= 0.5

  for (let i = 0; i < fullStars; i++) {
    stars += '<i class="fas fa-star text-warning"></i>'
  }

  if (hasHalf) {
    stars += '<i class="fas fa-star-half-alt text-warning"></i>'
    fullStars++
  }

  for (let i = fullStars; i < 5; i++) {
    stars += '<i class="far fa-star text-warning"></i>'
  }

  return stars
}

// ===== SCROLL TO TOP BUTTON =====
function createScrollToTopButton() {
  let btn = document.createElement('button')
  btn.id = 'scrollToTopBtn'
  btn.innerHTML = '<i class="fas fa-arrow-up"></i>'
  btn.style.cssText = `
    position: fixed;
    bottom: 30px;
    right: 30px;
    z-index: 9999;
    border: none;
    outline: none;
    background: var(--color-accent, #e94560);
    color: white;
    cursor: pointer;
    padding: 12px 16px;
    border-radius: 50%;
    font-size: 1.1rem;
    box-shadow: 0 4px 12px rgba(0,0,0,0.2);
    opacity: 0;
    visibility: hidden;
    transition: all 0.3s ease;
  `
  document.body.appendChild(btn)

  window.addEventListener('scroll', function () {
    if (window.scrollY > 300) {
      btn.style.opacity = '1'
      btn.style.visibility = 'visible'
    } else {
      btn.style.opacity = '0'
      btn.style.visibility = 'hidden'
    }
  })

  btn.addEventListener('click', function () {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  })
}

// ===== INITIALIZATION =====
document.addEventListener('DOMContentLoaded', function () {
  createScrollToTopButton()

  // Show TempData notification if present
  let tempMsg = document.getElementById('tempdata-message')
  if (tempMsg) {
    let msg = tempMsg.getAttribute('data-message')
    let type = tempMsg.getAttribute('data-type') || 'success'
    if (msg) {
      showNotification(msg, type)
    }
  }
})
