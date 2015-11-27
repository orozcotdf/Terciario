import 'toastr/build/toastr.min.css';
import './Notification.css';
import toastr from 'toastr';

export default class Notificaciones {
  static success(message, remove) {
    if (remove) {
      toastr.remove();
    }
    toastr.success(message);
  }

  static showLoading(message = 'Cargando...') {
    toastr.info(message, null, {timeOut: 0});
  }

  static error(message) {
    toastr.remove();
    toastr.error(message);
  }

  static clearNotifications() {
    toastr.remove();
  }


}
