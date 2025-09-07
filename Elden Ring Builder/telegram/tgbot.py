import telebot
import webbrowser
from telebot import types

bot = telebot.TeleBot('8265934634:AAHqrImNcCYIf4v7F7rkojILfMY5aya4gbA')

@bot.message_handler(commands=['site', 'website'])
def site(message):
    webbrowser.open('https://github.com/D1xer231/Elden-Ring-Builder')

@bot.message_handler(commands=['start'])
def main(message):
    bot.send_message(message.chat.id, f'Hello Turnished {message.from_user.first_name}, \nI am a Elden Ring Builder bot. 🤑\n\nTells us about problem\nyou have and we will help you solve it. ❓\n\nOr share your ideas with us. 💡')
    markup = types.InlineKeyboardMarkup()
    markup.add(types.InlineKeyboardButton('GitHub', url='https://github.com/D1xer231/Elden-Ring-Builder'))
    markup.add(types.InlineKeyboardButton('Steam', url='https://steamcommunity.com/profiles/76561199220453620/'))
    bot.reply_to(message, text='Useful links to find more about app', reply_markup=markup)

@bot.message_handler(commands=['bugs'])
def main(message):
    bot.send_message(message.chat.id, 'Write about your problem with app: 🤔')

@bot.message_handler(commands=['idea'])
def main(message):
    bot.send_message(message.chat.id, 'Share with us your idea: 🛎️')

@bot.message_handler(commands=['other'])
def main(message):
    bot.send_message(message.chat.id, 'Write your question: 📝')

license = """
MIT License

Copyright (c) [2025] [KapitanMoshonka]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE."""
@bot.message_handler(commands=['licence'])
def main(message):
    bot.send_message(message.chat.id, license)

@bot.message_handler()
def info(message):
    if message.text.lower() == 'hello':
        bot.send_message(message.chat.id, f'Hello Turnished {message.from_user.first_name}')
    if message.text.lower() == 'привет':
        bot.send_message(message.chat.id, f'Hello Turnished {message.from_user.first_name}')

@bot.message_handler(content_types=['photo','video','document', 'audio', 'voice', 'sticker', 'video_note', 'contact', 'location', 'venue'])
def get_photo(message):
    bot.reply_to(message, 'Only text ❌')

bot.polling(none_stop=True)